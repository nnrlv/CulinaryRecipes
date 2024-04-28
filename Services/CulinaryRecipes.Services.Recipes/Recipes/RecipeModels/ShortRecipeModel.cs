using AutoMapper;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CulinaryRecipes.Services.Recipes;

/// <summary>
/// Represents a model for a short recipe, including basic information.
/// </summary>
public class ShortRecipeModel
{
    /// <summary>
    /// The ID of the recipe.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The ID of the user who created the recipe.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The name of the recipe.
    /// </summary>
    public string Name { get; set; }
}

public class ShortRecipeModelProfile : Profile
{
    public ShortRecipeModelProfile()
    {
        CreateMap<Recipe, ShortRecipeModel>()
            .BeforeMap<ShortRecipeModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
    }
}

public class ShortRecipeModelActions : IMappingAction<Recipe, ShortRecipeModel>
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;

    public ShortRecipeModelActions(IDbContextFactory<MainDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public void Process(Recipe source, ShortRecipeModel destination, ResolutionContext context)
    {
        using var db = contextFactory.CreateDbContext();

        var recipe = db.Recipes.FirstOrDefault(x => x.Id == source.Id);

        var user = db.Users.FirstOrDefault(x => x.EntryId == source.UserId);

        if (recipe != null)
        {
            destination.Id = recipe.Uid;
            if (user != null) { destination.UserId = user.Id; }
        }
    }
}