﻿namespace CulinaryRecipes.Identity;

using CulinaryRecipes.Services.Settings;
using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            .AddLogSettings()
            ;

        return services;
    }
}
