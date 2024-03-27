namespace CulinaryRecipes.Workers.EmailSenderWorker;

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CulinaryRecipes.Services.EmailSender;
using CulinaryRecipes.Services.RabbitMq;
using CulinaryRecipes.Services.Actions;

public class TaskExecutor : ITaskExecutor
{
    private readonly ILogger<TaskExecutor> logger;
    private readonly IServiceProvider serviceProvider;
    private readonly IRabbitMq rabbitMq;

    public TaskExecutor(
        ILogger<TaskExecutor> logger,
        IServiceProvider serviceProvider,
        IRabbitMq rabbitMq
    )
    {
        this.logger = logger;
        this.serviceProvider = serviceProvider;
        this.rabbitMq = rabbitMq;
    }

    private async Task Execute<T>(Func<T, Task> action)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();

            var service = scope.ServiceProvider.GetService<T>();
            if (service != null)
                await action(service);
            else
                logger.LogError($"Error: {action} wasn't resolved");
        }
        catch (Exception e)
        {
            logger.LogError($"Error: {QueueNames.SEND_EMAIL}: {e.Message}");
            throw;
        }
    }

    public void Start()
    {
        rabbitMq.Subscribe<EmailModel>(QueueNames.SEND_EMAIL, async data
            => await Execute<IEmailSender>(async service =>
            {
                logger.LogDebug($"RABBITMQ::: {QueueNames.SEND_EMAIL}: {data.Email} {data.Message}");
                await service.SendEmailAsync(data);
            }));
    }
}