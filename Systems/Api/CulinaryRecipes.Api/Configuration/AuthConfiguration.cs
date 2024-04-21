namespace CulinaryRecipes.Api.Configuration;

using CulinaryRecipes.Common.Security;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using CulinaryRecipes.Services.Settings;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

public static class AuthConfiguration
{
    public static IServiceCollection AddAppAuth(this IServiceCollection services, IdentitySettings settings)
    {
        IdentityModelEventSource.ShowPII = true;

        services
            .AddIdentity<User, IdentityRole<Guid>>(opt =>
            {
                opt.Password.RequiredLength = 0;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<MainDbContext>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = settings.Url.StartsWith("https://");
                options.Authority = settings.Url;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.Audience = "api";
            });


        services.AddAuthorization(options =>
        {
            options.AddPolicy(AppScopes.IngredientsRead, policy => policy.RequireClaim("scope", AppScopes.IngredientsRead));
            options.AddPolicy(AppScopes.IngredientsWrite, policy => policy.RequireClaim("scope", AppScopes.IngredientsWrite));
            options.AddPolicy(AppScopes.UserAccountsRead, policy => policy.RequireClaim("scope", AppScopes.UserAccountsRead));
            options.AddPolicy(AppScopes.CategoriesRead, policy => policy.RequireClaim("scope", AppScopes.CategoriesRead));
            options.AddPolicy(AppScopes.CategoriesWrite, policy => policy.RequireClaim("scope", AppScopes.CategoriesWrite));
            options.AddPolicy(AppScopes.RecipesWrite, policy => policy.RequireClaim("scope", AppScopes.RecipesWrite));
        });

        return services;
    }

    public static IApplicationBuilder UseAppAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();

        app.UseAuthorization();

        return app;
    }
}