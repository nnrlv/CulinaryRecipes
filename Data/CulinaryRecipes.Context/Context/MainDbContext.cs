namespace CulinaryRecipes.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CulinaryRecipes.Context.Entities;

public class MainDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<IngredientInRecipe> IngredientsInRecipes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<RecipeInCategory> RecipesInCategories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

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
    }
}
