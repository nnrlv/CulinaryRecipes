using AutoMapper;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CulinaryRecipes.Services.Recipes;

/// <summary>
/// Represents a model for a full recipe, including detailed information.
/// </summary>
public class FullRecipeModel : ShortRecipeModel
{
    /// <summary>
    /// The preparation time of the recipe.
    /// </summary>
    public float PreparationTime { get; set; }

    /// <summary>
    /// The cooking time of the recipe.
    /// </summary>
    public float CookingTime { get; set; }

    /// <summary>
    /// The description of the recipe.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The instructions for preparing the recipe.
    /// </summary>
    public string Instructions { get; set; }

    /// <summary>
    /// The number of calories in the recipe.
    /// </summary>
    public float Calories { get; set; }

    /// <summary>
    /// The amount of proteins in the recipe.
    /// </summary>
    public float Proteins { get; set; }

    /// <summary>
    /// The amount of carbohydrates in the recipe.
    /// </summary>
    public float Carbohydrates { get; set; }

    /// <summary>
    /// The amount of fats in the recipe.
    /// </summary>
    public float Fats { get; set; }

    /// <summary>
    /// The IDs of the ingredients in the recipe.
    /// </summary>
    public ICollection<Guid>? IngredientsInRecipe { get; set; }

    /// <summary>
    /// The IDs of the categories associated with the recipe.
    /// </summary>
    public ICollection<Guid>? CategoriesInRecipe { get; set; }
}

public class FullRecipeModelProfile : Profile
{
    public FullRecipeModelProfile()
    {
        CreateMap<Recipe, FullRecipeModel>()
            .BeforeMap<FullRecipeModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
    }
}

public class FullRecipeModelActions : IMappingAction<Recipe, FullRecipeModel>
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;

    public FullRecipeModelActions(IDbContextFactory<MainDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public void Process(Recipe source, FullRecipeModel destination, ResolutionContext context)
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