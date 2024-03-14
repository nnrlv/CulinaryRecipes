namespace CulinaryRecipes.Services.Ingredients;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddIngredientService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IIngredientService, IngredientService>();
    }
}