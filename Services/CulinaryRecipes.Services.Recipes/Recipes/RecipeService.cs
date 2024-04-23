using AutoMapper;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using CulinaryRecipes.Services.Cache;
using Microsoft.EntityFrameworkCore;
using CulinaryRecipes.Common.Validator;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace CulinaryRecipes.Services.Recipes;

public class RecipeService : IRecipeService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateRecipeModel> createModelValidator;
    private readonly IModelValidator<UpdateRecipeModel> updateModelValidator;
    private readonly ICacheService cacheService;

    public RecipeService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<CreateRecipeModel> createModelValidator,
        IModelValidator<UpdateRecipeModel> updateModelValidator,
        ICacheService cacheService)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.createModelValidator = createModelValidator;
        this.updateModelValidator = updateModelValidator;
        this.cacheService = cacheService;
    }

    public async Task<IEnumerable<ShortRecipeModel>> GetAllShortRecipes()
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var recipes = await context.Recipes.ToListAsync();

        var result = mapper.Map<IEnumerable<ShortRecipeModel>>(recipes);

        return result;
    }

    public async Task<IEnumerable<ShortRecipeModel>> GetAllShortRecipesWithCaching()
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var cacheKey = "AllRecipes";

        var cachedRecipes = await cacheService.Get<IEnumerable<ShortRecipeModel>>(cacheKey);
        if (cachedRecipes != null)
        {
            return cachedRecipes;
        }

        var recipes = await context.Recipes.ToListAsync();

        var result = mapper.Map<IEnumerable<ShortRecipeModel>>(recipes);

        await cacheService.Put(cacheKey, result, TimeSpan.FromMinutes(5));

        return result;
    }

    public async Task<IEnumerable<ShortRecipeModel>> GetFilteredShortRecipesWithCaching(
        string? userName,
        float? minPreparationTime,
        float? maxPreparationTime,
        float? minCookingTime,
        float? maxCookingTime,
        string? categoryName)
    {

        await using var context = await dbContextFactory.CreateDbContextAsync();

        var cacheKey = "Filter";
        if (!string.IsNullOrEmpty(userName)) cacheKey += $"_userName_{userName}";
        if (minPreparationTime.HasValue) cacheKey += $"_minPreparationTime_{minPreparationTime}";
        if (maxPreparationTime.HasValue) cacheKey += $"_maxPreparationTime_{maxPreparationTime}";
        if (minCookingTime.HasValue) cacheKey += $"_minCookingTime_{minCookingTime}";
        if (maxCookingTime.HasValue) cacheKey += $"_maxCookingTime_{maxCookingTime}";
        if (!string.IsNullOrEmpty(categoryName)) cacheKey += $"_categoryName_{categoryName}";

        var cachedResult = await cacheService.Get<IEnumerable<ShortRecipeModel>>(cacheKey);
        if (cachedResult != null)
        {
            return cachedResult;
        }

        var query = context.Recipes.AsQueryable();

        if (!string.IsNullOrEmpty(userName))
        {
            query = query.Where(r => r.User.Name == userName);
        }

        if (minPreparationTime.HasValue)
        {
            query = query.Where(r => r.PreparationTime >= minPreparationTime.Value);
        }

        if (maxPreparationTime.HasValue)
        {
            query = query.Where(r => r.PreparationTime <= maxPreparationTime.Value);
        }

        if (minCookingTime.HasValue)
        {
            query = query.Where(r => r.CookingTime >= minCookingTime.Value);
        }

        if (maxCookingTime.HasValue)
        {
            query = query.Where(r => r.CookingTime <= maxCookingTime.Value);
        }

        if (!string.IsNullOrEmpty(categoryName))
        {
            query = query.Where(r => r.RecipesInCategories!.Any(ric => ric.Category.Name == categoryName));
        }

        var recipes = await query.ToListAsync();

        var result = mapper.Map<IEnumerable<ShortRecipeModel>>(recipes);

        await cacheService.Put(cacheKey, result, TimeSpan.FromMinutes(15));

        return result;
    }




    public async Task<ShortRecipeModel> GetShortRecipeById(Guid id)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var recipe = await context.Recipes.FirstOrDefaultAsync(x => x.Uid == id);

        var result = mapper.Map<ShortRecipeModel>(recipe);

        return result;
    }

    public async Task<ShortRecipeModel> GetShortRecipeByIdWithCaching(Guid id)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var cacheKey = $"ShortRecipe_{id}";

        var cachedRecipe = await cacheService.Get<ShortRecipeModel>(cacheKey);
        if (cachedRecipe != null)
        {
            return cachedRecipe;
        }

        var recipe = await context.Recipes.FirstOrDefaultAsync(x => x.Uid == id);

        var result = mapper.Map<ShortRecipeModel>(recipe);

        await cacheService.Put(cacheKey, result, TimeSpan.FromMinutes(5));

        return result;
    }

    public async Task<FullRecipeModel> GetFullRecipeById(Guid id)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var recipe = await context
            .Recipes
            .FirstOrDefaultAsync(x => x.Uid == id);

        if (recipe is null) { throw new InvalidOperationException("Recipe not found."); }

        var result = mapper.Map<FullRecipeModel>(recipe);
        var ingredientsWithRecipes = await GetIngredientsOfRecipe(recipe.Uid);

        result.IngredientsInRecipe = ingredientsWithRecipes
            .Select(x => x.IngredientId)
            .ToList();

        result.CategoriesInRecipe =
            mapper.Map<List<CategoryInRecipeModel>>
            (
                context.RecipesInCategories
                   .Where(x => x.RecipeId == recipe.Id)
                   .Include(x => x.Category)
                   .ToList()
            )
                .Select(x => x.CategoryId)
                .ToList();

        return result;
    }

    public async Task<FullRecipeModel> GetFullRecipeByIdWithCaching(Guid id)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var cacheKey = $"FullRecipe_{id}";

        var cachedRecipe = await cacheService.Get<FullRecipeModel>(cacheKey);
        if (cachedRecipe != null)
        {
            return cachedRecipe;
        }

        var recipe = await context
            .Recipes
            .FirstOrDefaultAsync(x => x.Uid == id);

        if (recipe is null) { throw new InvalidOperationException("Recipe not found."); }

        var result = mapper.Map<FullRecipeModel>(recipe);
        var ingredientsWithRecipes = await GetIngredientsOfRecipe(recipe.Uid);

        result.IngredientsInRecipe = ingredientsWithRecipes
            .Select(x => x.IngredientId)
            .ToList();

        result.CategoriesInRecipe =
            mapper.Map<List<CategoryInRecipeModel>>
            (
                context.RecipesInCategories
                   .Where(x => x.RecipeId == recipe.Id)
                   .Include(x => x.Category)
                   .ToList()
            )
                .Select(x => x.CategoryId)
                .ToList();

        await cacheService.Put(cacheKey, result, TimeSpan.FromMinutes(5));

        return result;
    }

    public async Task<IEnumerable<IngredientInRecipeModel>> GetIngredientsOfRecipe(Guid recipeId)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var recipe = context.Recipes.FirstOrDefault(x => x.Uid == recipeId);
        if (recipe is null) { throw new InvalidOperationException("Recipe not found."); }

        var ingredientsInRecipe = context.IngredientsInRecipes
                .Where(x => x.RecipeId == recipe.Id)
                .Include(x => x.Ingredient)
                .ToList();

        var data = mapper.Map<List<IngredientInRecipeModel>>(ingredientsInRecipe);

        return data;
    }

    public async Task<ShortRecipeModel> Create(CreateRecipeModel model, string userId)
    {
        createModelValidator.CheckAsync(model);

        await using var context = await dbContextFactory.CreateDbContextAsync();

        var user = await context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == userId);
        if (user is null) { throw new InvalidOperationException("User not found."); }

        var recipe = mapper.Map<Recipe>(model);
        recipe.UserId = user.EntryId;
        recipe.User = user;

        await context.Recipes.AddAsync(recipe);
        await context.SaveChangesAsync();

        return mapper.Map<ShortRecipeModel>(recipe);
    }

    public async Task<IngredientInRecipeModel> AddIngredientInRecipe(Guid recipeId, Guid ingredientId)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var recipe = context.Recipes.FirstOrDefault(x => x.Uid == recipeId);
        if (recipe is null) { throw new InvalidOperationException("Recipe not found."); }

        var ingredient = context.Ingredients.FirstOrDefault(x => x.Uid == ingredientId);
        if (ingredient is null) { throw new InvalidOperationException("Ingredient not found."); }

        var ingredientInRecipe = new IngredientInRecipe();
        ingredientInRecipe.RecipeId = recipe.Id;
        ingredientInRecipe.Recipe = recipe;
        ingredientInRecipe.IngredientId = ingredient.Id;
        ingredientInRecipe.Ingredient = ingredient;

        await context.IngredientsInRecipes.AddAsync(ingredientInRecipe);
        await context.SaveChangesAsync();

        var data = mapper.Map<IngredientInRecipeModel>(ingredientInRecipe);

        return data;
    }

    public async Task<CategoryInRecipeModel> AddCategoryToRecipe(Guid recipeId, Guid categoryId)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var recipe = context.Recipes.FirstOrDefault(x => x.Uid == recipeId);
        if (recipe is null) { throw new InvalidOperationException("Recipe not found."); }

        var category = context.Categories.FirstOrDefault(x => x.Uid == categoryId);
        if (category is null) { throw new InvalidOperationException("Category not found."); }

        var recipeInCategory = new RecipeInCategory();
        recipeInCategory.RecipeId = recipe.Id;
        recipeInCategory.Recipe = recipe;
        recipeInCategory.CategoryId = category.Id;
        recipeInCategory.Category = category;

        await context.RecipesInCategories.AddAsync(recipeInCategory);
        await context.SaveChangesAsync();

        var data = mapper.Map<CategoryInRecipeModel>(recipeInCategory);

        return data;
    }

    public async Task Update(Guid id, UpdateRecipeModel model)
    {
        updateModelValidator.Check(model);

        await using var context = await dbContextFactory.CreateDbContextAsync();

        var recipe = await context.Recipes.FirstOrDefaultAsync(x => x.Uid == id);
        if (recipe is null) { throw new InvalidOperationException("Recipe not found."); }

        recipe = mapper.Map<Recipe>(model);

        context.Recipes.Update(recipe);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var recipe = await context.Recipes.FirstOrDefaultAsync(x => x.Uid == id);
        if (recipe is null) { throw new InvalidOperationException("Recipe not found."); }

        context.Recipes.Remove(recipe);

        await context.SaveChangesAsync();
    }
}