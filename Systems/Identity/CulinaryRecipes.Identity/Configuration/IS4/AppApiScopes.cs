namespace CulinaryRecipes.Identity.Configuration;

using CulinaryRecipes.Common.Security;
using Duende.IdentityServer.Models;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(AppScopes.IngredientsRead, "Allowed to read ingredients data"),
            new ApiScope(AppScopes.IngredientsWrite, "Allowed to write ingredients data"),
            new ApiScope(AppScopes.UserAccountsRead, "Allowed to read user accounts data"),
            new ApiScope(AppScopes.CategoriesRead, "Allowed to read categories data"),
            new ApiScope(AppScopes.CategoriesWrite, "Allowed to write categories data"),
        };
}