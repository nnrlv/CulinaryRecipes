namespace CulinaryRecipes.Services.Subscriptions;

public interface ISubscriptionService
{
    Task SubscribeToUser(Guid subscriberId, Guid authorId);

    Task UnsubscribeFromUser(Guid subscriberId, Guid authorId);

    Task SubscribeToRecipe(Guid subscriberId, Guid recipeId);

    Task UnsubscribeFromRecipe(Guid subscriberId, Guid recipeId);
}
