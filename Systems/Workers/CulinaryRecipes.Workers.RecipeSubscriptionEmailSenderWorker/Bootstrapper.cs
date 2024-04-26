namespace CulinaryRecipes.Workers.RecipeSubscriptionEmailSenderWorker;

using Services.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using CulinaryRecipes.Services.EmailSender;
using CulinaryRecipes.Services.Logger;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddRabbitMq()
            .AddEmailSender()
            .AddAppLogger();

        services.AddSingleton<ITaskExecutor, TaskExecutor>();

        return services;
    }
}
