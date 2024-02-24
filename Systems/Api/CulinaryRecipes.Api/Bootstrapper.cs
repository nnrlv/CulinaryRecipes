using CulinaryRecipes.Services.Logger;
using CulinaryRecipes.Services.Settings;
using CulinaryRecipes.Context.Seeder;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices (this IServiceCollection service)
    {
        service
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddLogSettings()
            .AddAppLogger()
            .AddDbSeeder();
        return service;
    }
}
