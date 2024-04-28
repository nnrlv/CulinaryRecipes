namespace CulinaryRecipes.Context.Entities;

/// <summary>
/// Represents a subscription to a recipe, linking a subscriber to the recipe they are subscribed to.
/// </summary>
public class RecipeSubscription : BaseEntity
{
    /// <summary>
    /// The ID of the subscriber.
    /// </summary>
    public int SubscriberId { get; set; }

    /// <summary>
    /// The subscriber entity.
    /// </summary>
    public User Subscriber { get; set; }

    /// <summary>
    /// The ID of the recipe being subscribed to.
    /// </summary>
    public int RecipeId { get; set; }

    /// <summary>
    /// The recipe entity being subscribed to.
    /// </summary>
    public Recipe Recipe { get; set; }
}