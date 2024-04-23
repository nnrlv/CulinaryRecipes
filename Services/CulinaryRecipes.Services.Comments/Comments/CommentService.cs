using AutoMapper;
using CulinaryRecipes.Common.Exceptions;
using CulinaryRecipes.Common.Validator;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using CulinaryRecipes.Services.Cache;
using Microsoft.EntityFrameworkCore;

namespace CulinaryRecipes.Services.Comments;

public class CommentService : ICommentService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateCommentModel> createModelValidator;
    private readonly ICacheService cacheService;

    public CommentService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<CreateCommentModel> createModelValidator,
        ICacheService cacheService)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.createModelValidator = createModelValidator;
        this.cacheService = cacheService;
    }

    public async Task<IEnumerable<CommentModel>> GetAllByRecipeId(Guid recipeId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var comments = context
            .Comments
            .Include(x => x.Recipe)
            .Include(x => x.User)
            .Where(x => x.Recipe.Uid == recipeId)
            .AsQueryable();

        var result = (await comments.ToListAsync()).Select(comment => mapper.Map<CommentModel>(comment));

        return result;
    }

    public async Task<IEnumerable<CommentModel>> GetAllByRecipeIdWithCaching(Guid recipeId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var cacheKey = $"CommentsOfRecipe{recipeId}";

        var cachedComments = await cacheService.Get<IEnumerable<CommentModel>>(cacheKey);
        if (cachedComments != null)
        {
            return cachedComments;
        }

        var comments = context
            .Comments
            .Include(x => x.Recipe)
            .Include(x => x.User)
            .Where(x => x.Recipe.Uid == recipeId)
            .AsQueryable();

        var result = (await comments.ToListAsync()).Select(comment => mapper.Map<CommentModel>(comment));

        await cacheService.Put(cacheKey, result, TimeSpan.FromMinutes(5));

        return result;
    }

    public async Task<IEnumerable<CommentModel>> GetAllByUserId(Guid userId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var comments = context
             .Comments
             .Include(x => x.Recipe)
             .Include(x => x.User)
             .Where(x => x.User.Id == userId)
             .AsQueryable();

        var result = (await comments.ToListAsync()).Select(comment => mapper.Map<CommentModel>(comment));

        return result;
    }

    public async Task<IEnumerable<CommentModel>> GetAllByUserIdWithCaching(Guid userId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var cacheKey = $"CommentsOfUser{userId}";

        var cachedComments = await cacheService.Get<IEnumerable<CommentModel>>(cacheKey);
        if (cachedComments != null)
        {
            return cachedComments;
        }

        var comments = context
             .Comments
             .Include(x => x.Recipe)
             .Include(x => x.User)
             .Where(x => x.User.Id == userId)
             .AsQueryable();

        var result = (await comments.ToListAsync()).Select(comment => mapper.Map<CommentModel>(comment));

        await cacheService.Put(cacheKey, result, TimeSpan.FromMinutes(5));

        return result;
    }

    public async Task<CommentModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var comment = await context
            .Comments
            .Include(x => x.Recipe)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Uid.Equals(id));

        var result = mapper.Map<CommentModel>(comment);

        return result;
    }

    public async Task<CommentModel> GetByIdWithCaching(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var cacheKey = $"Comment_{id}";

        var cachedComment = await cacheService.Get<CommentModel>(cacheKey);
        if (cachedComment != null)
        {
            return cachedComment;
        }

        var comment = await context
            .Comments
            .Include(x => x.Recipe)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Uid.Equals(id));

        var result = mapper.Map<CommentModel>(comment);

        await cacheService.Put(cacheKey, result, TimeSpan.FromMinutes(5));

        return result;
    }

    public async Task<CommentModel> Create(CreateCommentModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var comment = mapper.Map<Comment>(model);
        await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();

        await context.Entry(comment).Reference(x => x.Recipe).LoadAsync();
        await context.Entry(comment).Reference(x => x.User).LoadAsync();

        return mapper.Map<CommentModel>(comment);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var comment = await context.Comments.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (comment == null)
            throw new ProcessException($"Comment (ID = {id}) not found.");

        context.Comments.Remove(comment);

        await context.SaveChangesAsync();
    }
}