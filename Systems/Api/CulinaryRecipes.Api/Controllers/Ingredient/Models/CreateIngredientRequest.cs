using AutoMapper;
using CulinaryRecipes.Services.Ingredients;
using FluentValidation;

namespace CulinaryRecipes.Api.Controllers.Ingredient;

public class CreateIngredientRequest
{
    public string Name { get; set; }
    public string UnitOfMeasurement { get; set; }

    public float Calories { get; set; }
    public float Proteins { get; set; }
    public float Carbohydrates { get; set; }
    public float Fats { get; set; }
}

public class CreateIngredientRequestProfile : Profile
{
    public CreateIngredientRequestProfile()
    {
        CreateMap<CreateIngredientRequest, CreateIngredientModel>();
    }
}