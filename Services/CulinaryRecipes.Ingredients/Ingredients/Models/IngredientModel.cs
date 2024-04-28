using AutoMapper;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CulinaryRecipes.Services.Ingredients;

/// <summary>
/// Represents a model for an ingredient.
/// </summary>
public class IngredientModel
{
    /// <summary>
    /// The ID of the ingredient.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the ingredient.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The unit of measurement for the ingredient.
    /// </summary>
    public string UnitOfMeasurement { get; set; }

    /// <summary>
    /// The number of calories per unit of the ingredient.
    /// </summary>
    public float Calories { get; set; }

    /// <summary>
    /// The amount of proteins per unit of the ingredient.
    /// </summary>
    public float Proteins { get; set; }

    /// <summary>
    /// The amount of carbohydrates per unit of the ingredient.
    /// </summary>
    public float Carbohydrates { get; set; }

    /// <summary>
    /// The amount of fats per unit of the ingredient.
    /// </summary>
    public float Fats { get; set; }
}

public class IngredientModelProfile : Profile
{
    public IngredientModelProfile()
    {
        CreateMap<Ingredient, IngredientModel>()
            .BeforeMap<IngredientModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}

public class IngredientModelActions : IMappingAction<Ingredient, IngredientModel>
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;

    public IngredientModelActions(IDbContextFactory<MainDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public void Process(Ingredient source, IngredientModel destination, ResolutionContext context)
    {
        using var db = contextFactory.CreateDbContext();

        var ingredient = db.Ingredients.FirstOrDefault(x => x.Id == source.Id);

        destination.Id = ingredient.Uid;
    }
}