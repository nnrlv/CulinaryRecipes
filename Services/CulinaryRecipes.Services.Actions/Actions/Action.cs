namespace CulinaryRecipes.Services.Actions;

using CulinaryRecipes.Services.EmailSender;
using CulinaryRecipes.Services.RabbitMq;
using System.Threading.Tasks;

public class Action : IAction
{
    private readonly IRabbitMq rabbitMq;

    public Action(IRabbitMq rabbitMq)
    {
        this.rabbitMq = rabbitMq;
    }

    public async Task SendEmail(EmailModel email)
    {
        await rabbitMq.PushAsync(QueueNames.SEND_EMAIL, email);
    }

    public async Task SendNewRecipeFromUserSubscriptionInfo(EmailModel email)
    {
        await rabbitMq.PushAsync(QueueNames.SEND_EMAIL_USER_SUBSCRIPTION, email);
    }

    public async Task SendNewCommentFromRecipeSubscriptionInfo(EmailModel email)
    {
        await rabbitMq.PushAsync(QueueNames.SEND_EMAIL_RECIPE_SUBSCRIPTION, email);
    }
}
