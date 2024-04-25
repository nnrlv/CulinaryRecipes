namespace CulinaryRecipes.Context.Entities;

public class RecipeSubscription : BaseEntity
{
    public int SubscriberId { get; set; }
    public User Subscriber { get; set; }

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}