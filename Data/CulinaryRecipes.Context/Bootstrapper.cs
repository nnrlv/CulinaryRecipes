namespace CulinaryRecipes.Context;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Settings;

/// <summary>
/// A static class for bootstrapping the application's database context.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds the application's database context to the specified IServiceCollection>.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the database context to.</param>
    /// <param name="configuration">The optional IConfiguration for loading database settings.</param>
    /// <returns>The modified IServiceCollection.</returns>
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Settings.Load<DbSettings>("Database", configuration);
        services.AddSingleton(settings);

        var dbInitOptionsDelegate = DbContextOptionsFactory.Configure(settings.ConnectionString, settings.Type, true);

        services.AddDbContextFactory<MainDbContext>(dbInitOptionsDelegate);

        return services;
    }
}