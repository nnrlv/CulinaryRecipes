using AutoMapper;
using CulinaryRecipes.Common.Exceptions;
using CulinaryRecipes.Common.Validator;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using CulinaryRecipes.Services.Cache;
using Microsoft.EntityFrameworkCore;

namespace CulinaryRecipes.Services.Categories;

/// <summary>
/// Service for managing categories.
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateCategoryModel> createModelValidator;
    private readonly IModelValidator<UpdateCategoryModel> updateModelValidator;
    private readonly ICacheService cacheService;

    public CategoryService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<CreateCategoryModel> createModelValidator,
        IModelValidator<UpdateCategoryModel> updateModelValidator,
        ICacheService cacheService)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.createModelValidator = createModelValidator;
        this.updateModelValidator = updateModelValidator;
        this.cacheService = cacheService;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CategoryModel>> GetAll()
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var categories = await context.Categories.ToListAsync();

        var result = mapper.Map<IEnumerable<CategoryModel>>(categories);

        return result;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CategoryModel>> GetAllWithCaching()
    {
        var cacheKey = "AllCategories";

        var cachedCategories = await cacheService.Get<IEnumerable<CategoryModel>>(cacheKey);
        if (cachedCategories != null)
        {
            return cachedCategories;
        }

        await using var context = await dbContextFactory.CreateDbContextAsync();

        var categories = await context.Categories.ToListAsync();

        var result = mapper.Map<IEnumerable<CategoryModel>>(categories);

        await cacheService.Put(cacheKey, result, TimeSpan.FromMinutes(5));

        return result;
    }

    /// <inheritdoc/>
    public async Task<CategoryModel> GetById(Guid id)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var category = await context.Categories.FirstOrDefaultAsync(x => x.Uid == id);

        var result = mapper.Map<CategoryModel>(category);

        return result;
    }

    /// <inheritdoc/>
    public async Task<CategoryModel> GetByIdWithCaching(Guid id)
    {
        var cacheKey = $"Category_{id}";

        var cachedCategory = await cacheService.Get<CategoryModel>(cacheKey);
        if (cachedCategory != null)
        {
            return cachedCategory;
        }

        await using var context = await dbContextFactory.CreateDbContextAsync();

        var category = await context.Categories.FirstOrDefaultAsync(x => x.Uid == id);

        var result = mapper.Map<CategoryModel>(category);

        await cacheService.Put(cacheKey, result, TimeSpan.FromMinutes(5));

        return result;
    }

    /// <inheritdoc/>
    public async Task<CategoryModel> Create(CreateCategoryModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var category = mapper.Map<Category>(model);

        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();

        return mapper.Map<CategoryModel>(category);
    }

    /// <inheritdoc/>
    public async Task Update(Guid id, UpdateCategoryModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var category = await context.Categories.Where(x => x.Uid == id).FirstOrDefaultAsync();

        category = mapper.Map(model, category);

        context.Categories.Update(category);

        await context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var category = await context.Categories.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (category == null)
            throw new ProcessException($"Category (ID = {id}) not found.");

        context.Categories.Remove(category);

        await context.SaveChangesAsync();
    }
}

