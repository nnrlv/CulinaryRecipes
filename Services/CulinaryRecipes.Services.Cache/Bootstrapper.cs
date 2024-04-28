namespace CulinaryRecipes.Services.Cache;

using CulinaryRecipes.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// A static class for bootstrapping caching in the service collection.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds caching services to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the caching services to.</param>
    /// <param name="configuration">The configuration to load cache settings from.</param>
    /// <returns>The modified IServiceCollection.</returns>
    public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var settings = Settings.Load<CacheSettings>("Cache", configuration);
        services.AddSingleton(settings);
        services.AddSingleton<ICacheService, CacheService>();
        return services;
    }
}