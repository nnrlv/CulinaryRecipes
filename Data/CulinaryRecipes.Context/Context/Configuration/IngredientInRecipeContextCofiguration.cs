namespace CulinaryRecipes.Context;

using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

public static class IngredientInRecipeContextConfiguration
{
    public static void ConfigureIngredientsInRecipes(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IngredientInRecipe>().ToTable("ingredients_in_recipes");

        modelBuilder.Entity<IngredientInRecipe>().
            HasOne(x => x.Ingredient)
           .WithMany(x => x.IngredientsInRecipe)
           .HasForeignKey(x => x.IngredientId)
           .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<IngredientInRecipe>().
            HasOne(x => x.Recipe)
           .WithMany(x => x.IngredientsInRecipe)
           .HasForeignKey(x => x.RecipeId)
           .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<IngredientInRecipe>().HasKey(x => new { x.IngredientId, x.RecipeId });
    }

}