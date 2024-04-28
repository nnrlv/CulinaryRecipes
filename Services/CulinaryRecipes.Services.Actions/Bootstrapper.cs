namespace CulinaryRecipes.Services.Actions;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// A static class for bootstrapping actions in the service collection.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds actions to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the actions to.</param>
    /// <returns>The modified IServiceCollection.</returns>
    public static IServiceCollection AddActions(this IServiceCollection services)
    {
        services.AddSingleton<IAction, Action>();

        return services;
    }
}
