using AutoMapper;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CulinaryRecipes.Services.Recipes;

/// <summary>
/// Represents an ingredient in a recipe model.
/// </summary>
public class IngredientInRecipeModel
{
    /// <summary>
    /// The ID of the recipe.
    /// </summary>
    public Guid RecipeId { get; set; }

    /// <summary>
    /// The ID of the ingredient.
    /// </summary>
    public Guid IngredientId { get; set; }
}


public class IngredientInRecipeModelProfile : Profile
{
    public IngredientInRecipeModelProfile()
    {
        CreateMap<IngredientInRecipe, IngredientInRecipeModel>()
            .BeforeMap<IngredientInRecipeModelActions>()
            .ForMember(dest => dest.RecipeId, opt => opt.Ignore())
            .ForMember(dest => dest.IngredientId, opt => opt.Ignore())
            .ReverseMap()
            .BeforeMap<ReverseIngredientInRecipeModelActions>()
            .ForMember(dest => dest.RecipeId, opt => opt.Ignore())
            .ForMember(dest => dest.IngredientId, opt => opt.Ignore())
            ;
    }
}

public class IngredientInRecipeModelActions : IMappingAction<IngredientInRecipe, IngredientInRecipeModel>
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;

    public IngredientInRecipeModelActions(IDbContextFactory<MainDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public void Process(IngredientInRecipe source, IngredientInRecipeModel destination, ResolutionContext context)
    {
        using var db = contextFactory.CreateDbContext();

        var recipe = db.Recipes.FirstOrDefault(x => x.Id == source.RecipeId);

        var ingredient = db.Ingredients.FirstOrDefault(x => x.Id == source.IngredientId);

        if (recipe != null && ingredient != null)
        {
            destination.RecipeId = recipe.Uid;
            destination.IngredientId = ingredient.Uid;
        }
    }
}

public class ReverseIngredientInRecipeModelActions : IMappingAction<IngredientInRecipeModel, IngredientInRecipe>
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;

    public ReverseIngredientInRecipeModelActions(IDbContextFactory<MainDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public void Process(IngredientInRecipeModel source, IngredientInRecipe destination, ResolutionContext context)
    {
        using var db = contextFactory.CreateDbContext();

        var recipe = db.Recipes.FirstOrDefault(x => x.Uid == source.RecipeId);

        var ingredient = db.Ingredients.FirstOrDefault(x => x.Uid == source.IngredientId);

        if (recipe != null && ingredient != null)
        {
            destination.RecipeId = recipe.Id;
            destination.IngredientId = ingredient.Id;
        }
    }
}