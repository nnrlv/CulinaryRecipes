namespace CulinaryRecipes.Context.Entities;

/// <summary>
/// Represents a category for recipes.
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// The name of the category.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The ID of the parent category.
    /// </summary>
    public int? CategoryId { get; set; }

    /// <summary>
    /// The parent category.
    /// </summary>
    public Category? ParentCategory { get; set; }

    /// <summary>
    /// The subcategories of this category.
    /// </summary>
    public virtual ICollection<Category>? Categories { get; set; }

    /// <summary>
    /// The recipes associated with this category.
    /// </summary>
    public virtual ICollection<RecipeInCategory>? RecipesInCategories { get; set; }
}
