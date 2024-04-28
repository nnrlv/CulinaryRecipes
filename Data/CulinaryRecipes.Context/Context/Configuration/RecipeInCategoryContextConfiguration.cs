namespace CulinaryRecipes.Context;

using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

public static class RecipeInCategoryContextConfiguration
{
    /// <summary>
    /// Configures the relationships and table mapping for the RecipeInCategory entity.
    /// </summary>
    /// <param name="modelBuilder">The ModelBuilder instance used to build the EF Core model.</param>
    public static void ConfigureRecipesInCategories(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecipeInCategory>().ToTable("recipes_in_categories");

        modelBuilder.Entity<RecipeInCategory>().
            HasOne(x => x.Recipe)
           .WithMany(x => x.RecipesInCategories)
           .HasForeignKey(x => x.RecipeId)
           .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<RecipeInCategory>().
            HasOne(x => x.Category)
           .WithMany(x => x.RecipesInCategories)
           .HasForeignKey(x => x.CategoryId)
           .OnDelete(DeleteBehavior.Cascade);
    }

}