namespace CulinaryRecipes.Services.UserAccounts;

using AutoMapper;
using CulinaryRecipes.Common.Exceptions;
using CulinaryRecipes.Common.Validator;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserAccountService : IUserAccountService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IModelValidator<CreateUserAccountModel> registerUserAccountModelValidator;

    public UserAccountService(
        IMapper mapper,
        IDbContextFactory<MainDbContext> dbContextFactory,
        UserManager<User> userManager, 
        IModelValidator<CreateUserAccountModel> registerUserAccountModelValidator
    )
    {
        this.mapper = mapper;
        this.dbContextFactory = dbContextFactory;
        this.userManager = userManager;
        this.registerUserAccountModelValidator = registerUserAccountModelValidator;
    }

    public async Task<bool> IsEmpty()
    {
        return !(await userManager.Users.AnyAsync());
    }

    public async Task<UserAccountModel> Create(CreateUserAccountModel model)
    {
        // TODO: добавить подтверждение электронной почты

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
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false           
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new ProcessException($"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");

        return mapper.Map<UserAccountModel>(user);
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
