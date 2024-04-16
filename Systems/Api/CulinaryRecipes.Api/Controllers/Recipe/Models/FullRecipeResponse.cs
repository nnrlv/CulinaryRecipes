using AutoMapper;
using CulinaryRecipes.Services.Recipes;

namespace CulinaryRecipes.Api.Controllers.Recipe;

public class FullRecipeResponse : ShortRecipeResponse
{
    public float PreparationTime { get; set; }
    public float CookingTime { get; set; }

    public string Description { get; set; }
    public string Instructions { get; set; }

    public float Calories { get; set; }
    public float Proteins { get; set; }
    public float Carbohydrates { get; set; }
    public float Fats { get; set; }

    public ICollection<Guid>? IngredientsInRecipe { get; set; }
    public ICollection<Guid>? CategoriesInRecipe { get; set; }
}

public class FullRecipeResponseProfile : Profile
{
    public FullRecipeResponseProfile()
    {
        CreateMap<FullRecipeModel, FullRecipeResponse>().ReverseMap();
    }
}