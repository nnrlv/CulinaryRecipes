namespace CulinaryRecipes.Services.Subscriptions;

/// <summary>
/// Interface for a service for managing subscriptions.
/// </summary>
public interface ISubscriptionService
{
    /// <summary>
    /// Subscribes a user to another user.
    /// </summary>
    Task SubscribeToUser(Guid subscriberId, Guid authorId);

    /// <summary>
    /// Unsubscribes a user from another user.
    /// </summary>
    Task UnsubscribeFromUser(Guid subscriberId, Guid authorId);

    /// <summary>
    /// Subscribes a user to a recipe.
    /// </summary>
    Task SubscribeToRecipe(Guid subscriberId, Guid recipeId);

    /// <summary>
    /// Unsubscribes a user from a recipe.
    /// </summary>
    Task UnsubscribeFromRecipe(Guid subscriberId, Guid recipeId);

    /// <summary>
    /// Sends an email to the subscribers of a user about a new recipe.
    /// </summary>
    Task SendEmailToUserSubscribersAboutNewRecipe(Guid authorId, string recipeName);

    /// <summary>
    /// Sends an email to the subscribers of a recipe about a new comment.
    /// </summary>
    Task SendEmailToRecipeSubscribersAboutNewComment(Guid recipeId);
}

