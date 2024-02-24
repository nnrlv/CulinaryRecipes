namespace CulinaryRecipes.Context;

using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

public static class CategoryContextConfiguration
{
    public static void ConfigureCategories(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().ToTable("categories");

        modelBuilder.Entity<Ingredient>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Ingredient>().Property(x => x.Name).HasMaxLength(50);

        modelBuilder.Entity<Category>()
           .HasOne(x => x.ParentCategory)
           .WithMany(x => x.Categories)
           .HasForeignKey(x => x.CategoryId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}