namespace CulinaryRecipes.Context;

using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

public static class RecipeSubscriptionContextConfiguration
{
    /// <summary>
    /// Configures the relationships and table mapping for the RecipeSubscription entity.
    /// </summary>
    /// <param name="modelBuilder">The ModelBuilder instance used to build the EF Core model.</param>
    public static void ConfigureRecipeSubscriptions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecipeSubscription>().ToTable("recipe_subscriptions");

        modelBuilder.Entity<RecipeSubscription>().
            HasOne(x => x.Recipe)
           .WithMany(x => x.RecipeSubscriptions)
           .HasForeignKey(x => x.RecipeId)
           .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<RecipeSubscription>().
            HasOne(x => x.Subscriber)
           .WithMany(x => x.RecipeSubscriptions)
           .HasForeignKey(x => x.SubscriberId)
           .HasPrincipalKey(x => x.EntryId)
           .OnDelete(DeleteBehavior.Cascade);
    }

}