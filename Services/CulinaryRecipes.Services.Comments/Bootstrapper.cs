using Microsoft.Extensions.DependencyInjection;

namespace CulinaryRecipes.Services.Comments;

public static class Bootstrapper
{
    /// <summary>
    /// Adds comments services to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the comments services to.</param>
    /// <returns>The modified IServiceCollection.</returns>
    public static IServiceCollection AddCommentService(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICommentService, CommentService>();
    }
}