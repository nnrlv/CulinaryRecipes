namespace CulinaryRecipes.Services.UserAccounts;

using AutoMapper;
using CulinaryRecipes.Common.Exceptions;
using CulinaryRecipes.Common.Validator;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using CulinaryRecipes.Services.Actions;
using CulinaryRecipes.Services.EmailSender;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserAccountService : IUserAccountService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IModelValidator<CreateUserAccountModel> registerUserAccountModelValidator;
    private readonly IAction action;

    public UserAccountService(
        IMapper mapper,
        IDbContextFactory<MainDbContext> dbContextFactory,
        UserManager<User> userManager,
        IModelValidator<CreateUserAccountModel> registerUserAccountModelValidator,
        IAction action
    )
    {
        this.mapper = mapper;
        this.dbContextFactory = dbContextFactory;
        this.userManager = userManager;
        this.registerUserAccountModelValidator = registerUserAccountModelValidator;
        this.action = action;
    }

    public async Task<bool> IsEmpty()
    {
        return !(await userManager.Users.AnyAsync());
    }

    public async Task<UserAccountModel> Create(CreateUserAccountModel model)
    {
        registerUserAccountModelValidator.Check(model);

        var user = await userManager.FindByEmailAsync(model.Email);
        if (user != null)
            throw new ProcessException($"User account with email {model.Email} already exist.");

        user = new User()
        {
            Status = UserStatus.Active,
            Name = model.Name,
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = false,
            PhoneNumber = null
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new ProcessException($"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");

        return mapper.Map<UserAccountModel>(user);
    }

    public async Task RequestEmailConfirmation(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            throw new ProcessException("User not found");
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var emailModel = new EmailModel
        {
            Email = email,
            Subject = "Confirm your account",
            Message = $"Please confirm your email by entering this token: " + token
        };

        await action.SendEmail(emailModel);
    }

    public async Task ConfirmEmail(string token, string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            throw new ProcessException("User not found");
        }

        if (user.EmailConfirmed) return;

        var result = await userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded == false)
        {
            throw new ProcessException("Incorrect confirmation");
        }
        else
        {
            user.EmailConfirmed = true;
        }
    }

    public async Task<IEnumerable<UserAccountModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var users = await context.Users
            .Include(x => x.Recipes)
            .ToListAsync();

        var result = mapper.Map<IEnumerable<UserAccountModel>>(users);

        return result;
    }

    public async Task<UserAccountModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var user = await context.Users
            .Include(x => x.Recipes)
            .FirstOrDefaultAsync(x => x.Id == id);

        var result = mapper.Map<UserAccountModel>(user);
        return result;
    }
}
