using CulinaryRecipes.Services.Logger;
using CulinaryRecipes.Services.Settings;
using CulinaryRecipes.Context.Seeder;
using CulinaryRecipes.Services.Ingredients;
using CulinaryRecipes.Services.UserAccounts;
using CulinaryRecipes.Services.RabbitMq;
using CulinaryRecipes.Services.Actions;
using CulinaryRecipes.Services.EmailSender;
using CulinaryRecipes.Services.Cache;

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
            .AddUserAccountService()
            .AddRabbitMq()
            .AddActions()
            .AddEmailSender()
            .AddCache();
        return service;
    }
}
