namespace CulinaryRecipes.Context.Entities;

/// <summary>
/// Represents an ingredient used in a recipe.
/// </summary>
public class IngredientInRecipe : BaseEntity
{
    /// <summary>
    /// The ID of the recipe.
    /// </summary>
    public int RecipeId { get; set; }

    /// <summary>
    /// The recipe that the ingredient belongs to.
    /// </summary>
    public Recipe Recipe { get; set; }

    /// <summary>
    /// The ID of the ingredient.
    /// </summary>
    public int IngredientId { get; set; }

    /// <summary>
    /// The ingredient in the recipe.
    /// </summary>
    public Ingredient Ingredient { get; set; }
}

