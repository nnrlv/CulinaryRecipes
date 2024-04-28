namespace CulinaryRecipes.Context.Entities;

/// <summary>
/// Represents a user subscription, linking a subscriber to an author they are subscribed to.
/// </summary>
public class UserSubscription : BaseEntity
{
    /// <summary>
    /// The ID of the subscriber.
    /// </summary>
    public int SubscriberId { get; set; }

    /// <summary>
    /// The ID of the author being subscribed to.
    /// </summary>
    public int AuthorId { get; set; }

    /// <summary>
    /// The author entity being subscribed to.
    /// </summary>
    public User Author { get; set; }
}
