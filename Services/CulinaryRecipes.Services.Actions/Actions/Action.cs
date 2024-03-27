namespace CulinaryRecipes.Services.Actions;

using CulinaryRecipes.Services.RabbitMq;
using CulinaryRecipes.Services.EmailSender;
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
}
