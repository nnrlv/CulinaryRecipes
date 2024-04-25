namespace CulinaryRecipes.Services.Subscriptions;
using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddSubscriptionService(this IServiceCollection services)
    {
        services.AddScoped<ISubscriptionService, SubscriptionService>();

        return services;
    }
}
