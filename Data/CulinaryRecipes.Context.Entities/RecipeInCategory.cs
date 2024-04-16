namespace CulinaryRecipes.Context.Entities;

public class RecipeInCategory : BaseEntity
{
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
