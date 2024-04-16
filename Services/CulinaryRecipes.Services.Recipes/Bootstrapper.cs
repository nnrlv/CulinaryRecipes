namespace CulinaryRecipes.Services.Recipes;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddRecipeService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IRecipeService, RecipeService>();
    }
}