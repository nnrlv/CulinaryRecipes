namespace CulinaryRecipes.Services.Actions;

/// <summary>
/// Provides queue names for different actions.
/// </summary>
public static class QueueNames
{
    /// <summary>
    /// The queue name for sending user verification emails.
    /// </summary>
    public const string SEND_EMAIL = "CULINARYRECIPES_SEND_EMAIL";

    /// <summary>
    /// The queue name for sending notification about new recipes on the user subscriptions.
    /// </summary>
    public const string SEND_EMAIL_USER_SUBSCRIPTION = "CULINARYRECIPES_SEND_EMAIL_USER_SUBSCRIPTION";

    /// <summary>
    /// The queue name for sending notification about new comments on the recipe subscriptions.
    /// </summary>
    public const string SEND_EMAIL_RECIPE_SUBSCRIPTION = "CULINARYRECIPES_SEND_EMAIL_RECIPE_SUBSCRIPTION";
}

