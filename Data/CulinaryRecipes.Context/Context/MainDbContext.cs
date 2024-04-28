namespace CulinaryRecipes.Context;

using CulinaryRecipes.Context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Represents the main database context for the application, derived from IdentityDbContext
/// </summary>
public class MainDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    /// <summary>
    /// DbSet of recipes entites
    /// </summary>
    public DbSet<Recipe> Recipes { get; set; }
    /// <summary>
    /// DbSet of ingredients entites
    /// </summary>
    public DbSet<Ingredient> Ingredients { get; set; }
    /// <summary>
    /// DbSet of join entities between recipes and ingredients
    /// </summary>
    public DbSet<IngredientInRecipe> IngredientsInRecipes { get; set; }
    /// <summary>
    /// DbSet of categories entites
    /// </summary>
    public DbSet<Category> Categories { get; set; }
    /// <summary>
    /// DbSet of join entities between recipes and categories
    /// </summary>
    public DbSet<RecipeInCategory> RecipesInCategories { get; set; }
    /// <summary>
    /// DbSet of users entites
    /// </summary>
    public DbSet<User> Users { get; set; }
    /// <summary>
    /// DbSet of comments entites
    /// </summary>
    public DbSet<Comment> Comments { get; set; }
    /// <summary>
    /// DbSet of user subscriptions entites (when user subscribes to the author, 
    /// he completes this subscription and receives email notifications about new recipes from the author)
    /// </summary>
    public DbSet<UserSubscription> UserSubscriptions { get; set; }
    /// <summary>
    /// DbSet of recipe subscriptions entites (when user subscribes to the recipe, 
    /// he completes this subscription and receives email notifications about new comments under this recipe)
    /// </summary>
    public DbSet<RecipeSubscription> RecipeSubscriptions { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    /// <summary>
    /// Configures the entity mappings for the database context using the provided <paramref name="modelBuilder"/>.
    /// </summary>
    /// <param name="modelBuilder">The <see cref="ModelBuilder"/> instance used to configure the entity mappings.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureRecipes();
        modelBuilder.ConfigureIngredients();
        modelBuilder.ConfigureIngredientsInRecipes();
        modelBuilder.ConfigureCategories();
        modelBuilder.ConfigureRecipesInCategories();
        modelBuilder.ConfigureUsers();
        modelBuilder.ConfigureComments();
        modelBuilder.ConfigureUserSubscriptions();
        modelBuilder.ConfigureRecipeSubscriptions();

    }
}
