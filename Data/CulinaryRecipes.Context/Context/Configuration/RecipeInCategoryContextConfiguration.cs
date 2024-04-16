namespace CulinaryRecipes.Context;

using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

public static class RecipeInCategoryContextConfiguration
{
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