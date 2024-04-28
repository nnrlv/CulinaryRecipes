namespace CulinaryRecipes.Context.Entities;

/// <summary>
/// Represents a mapping between a recipe and a category it belongs to.
/// </summary>
public class RecipeInCategory : BaseEntity
{
    /// <summary>
    /// The ID of the recipe.
    /// </summary>
    public int RecipeId { get; set; }

    /// <summary>
    /// The recipe.
    /// </summary>
    public Recipe Recipe { get; set; }

    /// <summary>
    /// The ID of the category.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// The category.
    /// </summary>
    public Category Category { get; set; }
}
