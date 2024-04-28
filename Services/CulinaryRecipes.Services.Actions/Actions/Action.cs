namespace CulinaryRecipes.Services.Actions;

using CulinaryRecipes.Services.EmailSender;
using CulinaryRecipes.Services.RabbitMq;
using System.Threading.Tasks;

/// <summary>
/// Represents an action to be performed, such as sending emails.
/// </summary>
public class Action : IAction
{
    private readonly IRabbitMq rabbitMq;

    /// <summary>
    /// Initializes a new instance of the Action class with the specified RabbitMQ service.
    /// </summary>
    /// <param name="rabbitMq">The RabbitMQ service.</param>
    public Action(IRabbitMq rabbitMq)
    {
        this.rabbitMq = rabbitMq;
    }

    /// <inheritdoc/>
    public async Task SendEmail(EmailModel email)
    {
        await rabbitMq.PushAsync(QueueNames.SEND_EMAIL, email);
    }

    /// <inheritdoc/>
    public async Task SendNewRecipeFromUserSubscriptionInfo(EmailModel email)
    {
        await rabbitMq.PushAsync(QueueNames.SEND_EMAIL_USER_SUBSCRIPTION, email);
    }

    /// <inheritdoc/>
    public async Task SendNewCommentFromRecipeSubscriptionInfo(EmailModel email)
    {
        await rabbitMq.PushAsync(QueueNames.SEND_EMAIL_RECIPE_SUBSCRIPTION, email);
    }
}
