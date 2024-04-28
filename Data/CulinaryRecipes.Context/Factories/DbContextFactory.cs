namespace CulinaryRecipes.Context;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Factory for creating instances of MainDbContext.
/// </summary>
public class DbContextFactory
{
    private readonly DbContextOptions<MainDbContext> options;

    /// <summary>
    /// Initializes a new instance of the DbContextFactory class.
    /// </summary>
    /// <param name="options">The options to be used for creating the MainDbContext.</param>
    public DbContextFactory(DbContextOptions<MainDbContext> options)
    {
        this.options = options;
    }

    /// <summary>
    /// Creates a new instance of MainDbContext.
    /// </summary>
    /// <returns>The created MainDbContext instance.</returns>
    public MainDbContext Create()
    {
        return new MainDbContext(options);
    }
}