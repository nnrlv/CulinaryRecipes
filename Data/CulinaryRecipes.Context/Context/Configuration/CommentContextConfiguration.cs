namespace CulinaryRecipes.Context;

using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

public static class CommentContextConfiguration
{
    public static void ConfigureComments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>().ToTable("comments");

        modelBuilder.Entity<Comment>().
            HasOne(x => x.User)
           .WithMany(x => x.Comments)
           .HasForeignKey(x => x.UserId)
           .HasPrincipalKey(x => x.EntryId);

        modelBuilder.Entity<Comment>().
            HasOne(x => x.Recipe)
           .WithMany(x => x.Comments)
           .HasForeignKey(x => x.RecipeId);
    }

}