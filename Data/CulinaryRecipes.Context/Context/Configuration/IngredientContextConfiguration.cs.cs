namespace CulinaryRecipes.Context;

using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

public static class IngredientContextConfiguration
{
    /// <summary>
    /// Configures the relationships and table mapping for the Ingredient entity.
    /// </summary>
    /// <param name="modelBuilder">The ModelBuilder instance used to build the EF Core model.</param>
    public static void ConfigureIngredients(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingredient>().ToTable("ingredients");

        modelBuilder.Entity<Ingredient>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Ingredient>().Property(x => x.Name).HasMaxLength(50);
    }
}