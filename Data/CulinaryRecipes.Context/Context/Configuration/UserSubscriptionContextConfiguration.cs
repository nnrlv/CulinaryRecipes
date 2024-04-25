namespace CulinaryRecipes.Context;

using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

public static class UserSubscriptionContextConfiguration
{
    public static void ConfigureUserSubscriptions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserSubscription>().ToTable("user_subscriptions");

        modelBuilder.Entity<UserSubscription>().
            HasOne(x => x.Author)
           .WithMany(x => x.UserSubscriptions)
           .HasForeignKey(x => x.AuthorId)
           .HasPrincipalKey(x => x.EntryId)
           .OnDelete(DeleteBehavior.NoAction);
    }

}