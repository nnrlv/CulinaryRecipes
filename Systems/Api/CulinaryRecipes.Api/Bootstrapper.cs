using CulinaryRecipes.Services.Logger;
using CulinaryRecipes.Services.Settings;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices (this IServiceCollection service)
    {
        service
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddLogSettings()
            .AddAppLogger();
        return service;
    }
}
