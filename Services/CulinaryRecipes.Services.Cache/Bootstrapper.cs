namespace CulinaryRecipes.Services.Cache;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CulinaryRecipes.Settings;

public static class Bootstrapper
{
    public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var settings = Settings.Load<CacheSettings>("Cache", configuration);
        services.AddSingleton(settings);
        services.AddSingleton<ICacheService, CacheService>();
        return services;
    }
}