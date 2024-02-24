namespace CulinaryRecipes.Context;

using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

public static class IngredientContextConfiguration
{
    public static void ConfigureIngredients(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingredient>().ToTable("ingredients");

        modelBuilder.Entity<Ingredient>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Ingredient>().Property(x => x.Name).HasMaxLength(50);
    }
}