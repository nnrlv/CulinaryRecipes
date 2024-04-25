namespace CulinaryRecipes.Context;

using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

public static class RecipeSubscriptionContextConfiguration
{
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