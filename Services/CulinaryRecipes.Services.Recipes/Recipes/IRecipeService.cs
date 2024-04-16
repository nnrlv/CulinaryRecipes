using CulinaryRecipes.Context.Entities;

namespace CulinaryRecipes.Services.Recipes;

public interface IRecipeService
{
    Task<IEnumerable<ShortRecipeModel>> GetAllShortRecipes();

    Task<IEnumerable<ShortRecipeModel>> GetAllShortRecipesWithCaching();

    Task<ShortRecipeModel> GetShortRecipeById(Guid id);

    Task<ShortRecipeModel> GetShortRecipeByIdWithCaching(Guid id);

    Task<FullRecipeModel> GetFullRecipeById(Guid id);

    Task<FullRecipeModel> GetFullRecipeByIdWithCaching(Guid id);

    Task<IEnumerable<IngredientInRecipeModel>> GetIngredientsOfRecipe(Guid recipeId);

    Task<ShortRecipeModel> Create(CreateRecipeModel model, string userId);

    Task<IngredientInRecipeModel> AddIngredientInRecipe(Guid recipeId, Guid ingredientId);

    Task<CategoryInRecipeModel> AddCategoryToRecipe(Guid recipeId, Guid categoryId);

    Task Update(Guid id, UpdateRecipeModel model);

    Task Delete(Guid id);
}