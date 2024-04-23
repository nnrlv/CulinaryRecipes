namespace CulinaryRecipes.Context.Entities;

public class Comment : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }

    public string Text { get; set; }
}
