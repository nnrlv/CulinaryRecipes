using AutoMapper;
using CulinaryRecipes.Services.Recipes;

namespace CulinaryRecipes.Api.Controllers.Recipe;

public class CreateRecipeRequest
{
    public string Name { get; set; }

    public float PreparationTime { get; set; }
    public float CookingTime { get; set; }

    public string Description { get; set; }
    public string Instructions { get; set; }

    public float Calories { get; set; }
    public float Proteins { get; set; }
    public float Carbohydrates { get; set; }
    public float Fats { get; set; }
}

public class CreateRecipeRequestProfile : Profile
{
    public CreateRecipeRequestProfile()
    {
        CreateMap<CreateRecipeRequest, CreateRecipeModel>();
    }
}