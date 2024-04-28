namespace CulinaryRecipes.Context;

using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

public static class IngredientInRecipeContextConfiguration
{
    /// <summary>
    /// Configures the relationships and table mapping for the IngredientInRecipe entity.
    /// </summary>
    /// <param name="modelBuilder">The ModelBuilder instance used to build the EF Core model.</param>
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
    }

}