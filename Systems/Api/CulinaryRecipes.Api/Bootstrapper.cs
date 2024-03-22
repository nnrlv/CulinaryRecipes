using CulinaryRecipes.Services.Logger;
using CulinaryRecipes.Services.Settings;
using CulinaryRecipes.Context.Seeder;
using CulinaryRecipes.Services.Ingredients;
using CulinaryRecipes.Services.UserAccounts;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices (this IServiceCollection service)
    {
        service
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddLogSettings()
            .AddIdentitySettings()
            .AddAppLogger()
            .AddDbSeeder()
            .AddIngredientService()
            .AddUserAccountService();
        return service;
    }
}
