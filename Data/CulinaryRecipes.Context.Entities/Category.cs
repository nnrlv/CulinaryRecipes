namespace CulinaryRecipes.Context.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }

    public int? CategoryId { get; set; }
    public Category? ParentCategory { get; set; }

    public virtual ICollection<Category>? Categories { get; set; }
    public virtual ICollection<RecipeInCategory>? RecipesInCategories { get; set; }
}
