namespace CulinaryRecipes.Context;

using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

public static class RecipeContextConfiguration
{
    /// <summary>
    /// Configures the relationships and table mapping for the Recipe entity.
    /// </summary>
    /// <param name="modelBuilder">The ModelBuilder instance used to build the EF Core model.</param>
    public static void ConfigureRecipes(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>().ToTable("recipes");

        modelBuilder.Entity<Recipe>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Recipe>().Property(x => x.Name).HasMaxLength(50);

        modelBuilder.Entity<Recipe>().Property(x => x.Description).HasMaxLength(100);

        modelBuilder.Entity<Recipe>().Property(x => x.Instructions).HasMaxLength(200);

        modelBuilder.Entity<Recipe>()
            .HasOne(x => x.User)
            .WithMany(x => x.Recipes)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.EntryId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}