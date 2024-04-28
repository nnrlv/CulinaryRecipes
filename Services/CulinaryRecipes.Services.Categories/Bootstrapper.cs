namespace CulinaryRecipes.Services.Categories;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    /// <summary>
    /// Adds categories services to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the caching services to.</param>
    /// <returns>The modified IServiceCollection.</returns>
    public static IServiceCollection AddCategoryService(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICategoryService, CategoryService>();
    }
}