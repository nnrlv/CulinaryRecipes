using AutoMapper;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CulinaryRecipes.Services.Ingredients;

public class IngredientModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string UnitOfMeasurement { get; set; }

    public float Calories { get; set; }
    public float Proteins { get; set; }
    public float Carbohydrates { get; set; }
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