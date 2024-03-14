using AutoMapper;
using CulinaryRecipes.Services.Ingredients;

namespace CulinaryRecipes.Api.Controllers.Ingredient;

public class IngredientResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string UnitOfMeasurement { get; set; }

    public float Calories { get; set; }
    public float Proteins { get; set; }
    public float Carbohydrates { get; set; }
    public float Fats { get; set; }
}

public class IngredientResponseProfile : Profile
{
    public IngredientResponseProfile()
    {
        CreateMap<IngredientModel, IngredientResponse>().ReverseMap();
    }
}