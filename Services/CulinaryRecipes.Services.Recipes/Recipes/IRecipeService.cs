namespace CulinaryRecipes.Services.Recipes;

/// <summary>
/// Interface of service for managing recipes.
/// </summary>
public interface IRecipeService
{
    /// <summary>
    /// Gets all recipes in a short format.
    /// </summary>
    Task<IEnumerable<ShortRecipeModel>> GetAllShortRecipes();

    /// <summary>
    /// Gets all recipes in a short format with caching.
    /// </summary>
    Task<IEnumerable<ShortRecipeModel>> GetAllShortRecipesWithCaching();

    /// <summary>
    /// Gets filtered recipes in a short format with caching based on the specified criteria.
    /// </summary>
    /// <param name="userName">The name of the user who created the recipe.</param>
    /// <param name="minPreparationTime">The minimum preparation time of the recipe.</param>
    /// <param name="maxPreparationTime">The maximum preparation time of the recipe.</param>
    /// <param name="minCookingTime">The minimum cooking time of the recipe.</param>
    /// <param name="maxCookingTime">The maximum cooking time of the recipe.</param>
    /// <param name="categoryName">The name of the category the recipe belongs to.</param>
    /// <returns>The filtered recipes.</returns>
    Task<IEnumerable<ShortRecipeModel>> GetFilteredShortRecipesWithCaching(
        string? userName,
        float? minPreparationTime,
        float? maxPreparationTime,
        float? minCookingTime,
        float? maxCookingTime,
        string? categoryName);

    /// <summary>
    /// Gets a recipe in a short format by its ID.
    /// </summary>
    /// <param name="id">The ID of the recipe.</param>
    /// <returns>The recipe in a short format.</returns>
    Task<ShortRecipeModel> GetShortRecipeById(Guid id);

    /// <summary>
    /// Gets a recipe in a short format by its ID with caching.
    /// </summary>
    /// <param name="id">The ID of the recipe.</param>
    /// <returns>The recipe in a short format.</returns>
    Task<ShortRecipeModel> GetShortRecipeByIdWithCaching(Guid id);

    /// <summary>
    /// Gets a recipe in a full format by its ID.
    /// </summary>
    /// <param name="id">The ID of the recipe.</param>
    /// <returns>The recipe in a full format.</returns>
    Task<FullRecipeModel> GetFullRecipeById(Guid id);

    /// <summary>
    /// Gets a recipe in a full format by its ID with caching.
    /// </summary>
    /// <param name="id">The ID of the recipe.</param>
    /// <returns>The recipe in a full format.</returns>
    Task<FullRecipeModel> GetFullRecipeByIdWithCaching(Guid id);

    /// <summary>
    /// Gets the ingredients of a recipe by its ID.
    /// </summary>
    /// <param name="recipeId">The ID of the recipe.</param>
    /// <returns>The ingredients of the recipe.</returns>
    Task<IEnumerable<IngredientInRecipeModel>> GetIngredientsOfRecipe(Guid recipeId);

    /// <summary>
    /// Creates a new recipe based on the specified model.
    /// </summary>
    /// <param name="model">The model containing information about the recipe to create.</param>
    /// <param name="userId">The ID of the user creating the recipe.</param>
    /// <returns>The created recipe in a short format.</returns>
    Task<ShortRecipeModel> Create(CreateRecipeModel model, string userId);

    /// <summary>
    /// Adds an ingredient to a recipe.
    /// </summary>
    /// <param name="recipeId">The ID of the recipe.</param>
    /// <param name="ingredientId">The ID of the ingredient to add.</param>
    /// <returns>The ingredient in recipe model.</returns>
    Task<IngredientInRecipeModel> AddIngredientInRecipe(Guid recipeId, Guid ingredientId);

    /// <summary>
    /// Adds a category to a recipe.
    /// </summary>
    /// <param name="recipeId">The ID of the recipe.</param>
    /// <param name="categoryId">The ID of the category to add.</param>
    /// <returns>The category in recipe model.</returns>
    Task<CategoryInRecipeModel> AddCategoryToRecipe(Guid recipeId, Guid categoryId);

    /// <summary>
    /// Updates an existing recipe with the specified ID using the information from the provided model.
    /// </summary>
    /// <param name="id">The ID of the recipe to update.</param>
    /// <param name="model">The model containing the updated information for the recipe.</param>
    Task Update(Guid id, UpdateRecipeModel model);

    /// <summary>
    /// Deletes the recipe with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the recipe to delete.</param>
    Task Delete(Guid id);
}