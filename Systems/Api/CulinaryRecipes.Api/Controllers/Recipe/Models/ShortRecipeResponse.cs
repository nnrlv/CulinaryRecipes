using AutoMapper;
using CulinaryRecipes.Services.Recipes;

namespace CulinaryRecipes.Api.Controllers.Recipe;

public class ShortRecipeResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
}

public class ShortRecipeResponseProfile : Profile
{
    public ShortRecipeResponseProfile()
    {
        CreateMap<ShortRecipeModel, ShortRecipeResponse>().ReverseMap();
    }
}