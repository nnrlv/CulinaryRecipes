using AutoMapper;
using CulinaryRecipes.Services.Ingredients;

namespace CulinaryRecipes.Api.Controllers.Ingredient;

public class UpdateIngredientRequest
{
    public string Name { get; set; }
    public string UnitOfMeasurement { get; set; }
}

public class UpdateIngredientRequestProfile : Profile
{
    public UpdateIngredientRequestProfile()
    {
        CreateMap<UpdateIngredientRequest, UpdateIngredientModel>();
    }
}