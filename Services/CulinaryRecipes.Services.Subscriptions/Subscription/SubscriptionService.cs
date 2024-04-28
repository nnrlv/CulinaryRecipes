using AutoMapper;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;
using CulinaryRecipes.Services.Actions;
using CulinaryRecipes.Services.EmailSender;

namespace CulinaryRecipes.Services.Subscriptions;

/// <summary>
/// Service for managing subscriptions.
/// </summary>
public class SubscriptionService : ISubscriptionService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAction action;

    public SubscriptionService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper,
        IAction action)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.action = action;
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public async Task SendEmailToUserSubscribersAboutNewRecipe(Guid authorId, string recipeName)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var author = await context
            .Users
            .FirstOrDefaultAsync(x => x.Id == authorId);

        if (author is null) throw new InvalidOperationException("Author not found");

        var subscribers = await context
            .UserSubscriptions
            .Where(x => x.AuthorId == author.EntryId)
            .Select(x => x.SubscriberId)
            .ToListAsync();

        var subscriberEmails = await context
            .Users
            .Where(x => subscribers.Contains(x.EntryId))
            .Select(x => x.Email)
            .ToListAsync();

        foreach (var email in subscriberEmails)
        {
            await action.SendNewRecipeFromUserSubscriptionInfo(new EmailModel
            {
                Email = email,
                Subject = $"Check new recipe from {author.Name}",
                Message = $"User that you subscribed {author.Name} posted a new recipe named {recipeName}."
            });
        }
    }

    /// <inheritdoc/>
    public async Task SendEmailToRecipeSubscribersAboutNewComment(Guid recipeId)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var recipe = await context
            .Recipes
            .FirstOrDefaultAsync(x => x.Uid == recipeId);

        if (recipe is null) throw new InvalidOperationException("Recipe not found");

        var subscribers = await context
            .RecipeSubscriptions
            .Where(x => x.RecipeId == recipe.Id)
            .Select(x => x.SubscriberId)
            .ToListAsync();

        var subscriberEmails = await context
            .Users
            .Where(x => subscribers.Contains(x.EntryId))
            .Select(x => x.Email)
            .ToListAsync();

        foreach (var email in subscriberEmails)
        {
            await action.SendNewCommentFromRecipeSubscriptionInfo(new EmailModel
            {
                Email = email,
                Subject = $"Check new comment in recipe {recipe.Name}",
                Message = $"New comment posted on a recipe that you subscribed: {recipe.Name}."
            });
        }
    }
}

