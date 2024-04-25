using AutoMapper;
using CulinaryRecipes.Common.Exceptions;
using CulinaryRecipes.Common.Validator;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CulinaryRecipes.Services.Subscriptions;

public class SubscriptionService : ISubscriptionService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public SubscriptionService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task SubscribeToUser(Guid subscriberId, Guid authorId)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var subscriber = await context
            .Users
            .FirstOrDefaultAsync(x => x.Id == subscriberId);

        if (subscriber is null) throw new InvalidOperationException("Subscriber not found");

        var author = await context
            .Users
            .FirstOrDefaultAsync(x => x.Id == authorId);

        if (author is null) throw new InvalidOperationException("Author not found");

        var subscription = await context
            .UserSubscriptions
            .FirstOrDefaultAsync(x => x.AuthorId == author.EntryId && x.SubscriberId == subscriber.EntryId);

        if (subscription is not null) throw new InvalidOperationException("Subscription already exists");

        subscription = new UserSubscription()
        {
            SubscriberId = subscriber.EntryId,
            AuthorId = author.EntryId
        };

        await context.UserSubscriptions.AddAsync(subscription);
        await context.SaveChangesAsync();
    }

    public async Task UnsubscribeFromUser(Guid subscriberId, Guid authorId)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var subscriber = await context
            .Users
            .FirstOrDefaultAsync(x => x.Id == subscriberId);

        if (subscriber is null) throw new InvalidOperationException("Subscriber not found");

        var author = await context
            .Users
            .FirstOrDefaultAsync(x => x.Id == authorId);

        if (author is null) throw new InvalidOperationException("Author not found");

        var subscription = await context
            .UserSubscriptions
            .FirstOrDefaultAsync(x => x.AuthorId == author.EntryId && x.SubscriberId == subscriber.EntryId);

        if (subscription is null) throw new InvalidOperationException("Subscription not found");

        context.UserSubscriptions.Remove(subscription);
        await context.SaveChangesAsync();
    }

    public async Task SubscribeToRecipe(Guid subscriberId, Guid recipeId)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var subscriber = await context
            .Users
            .FirstOrDefaultAsync(x => x.Id == subscriberId);

        if (subscriber is null) throw new InvalidOperationException("Subscriber not found");

        var recipe = await context
            .Recipes
            .FirstOrDefaultAsync(x => x.Uid == recipeId);

        if (recipe is null) throw new InvalidOperationException("Recipe not found");

        var subscription = await context
            .RecipeSubscriptions
            .FirstOrDefaultAsync(x => x.RecipeId == recipe.Id && x.SubscriberId == subscriber.EntryId);

        if (subscription is not null) throw new InvalidOperationException("Subscription already exists");

        subscription = new RecipeSubscription()
        {
            SubscriberId = subscriber.EntryId,
            RecipeId = recipe.Id
        };

        await context.RecipeSubscriptions.AddAsync(subscription);
        await context.SaveChangesAsync();
    }

    public async Task UnsubscribeFromRecipe(Guid subscriberId, Guid recipeId)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var subscriber = await context
            .Users
            .FirstOrDefaultAsync(x => x.Id == subscriberId);

        if (subscriber is null) throw new InvalidOperationException("Subscriber not found");

        var recipe = await context
            .Recipes
            .FirstOrDefaultAsync(x => x.Uid == recipeId);

        if (recipe is null) throw new InvalidOperationException("Recipe not found");

        var subscription = await context
            .RecipeSubscriptions
            .FirstOrDefaultAsync(x => x.RecipeId == recipe.Id && x.SubscriberId == subscriber.EntryId);

        if (subscription is null) throw new InvalidOperationException("Subscription not found");

        context.RecipeSubscriptions.Remove(subscription);
        await context.SaveChangesAsync();
    }
}

