using AutoMapper;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CulinaryRecipes.Services.Recipes;

public class FullRecipeModel : ShortRecipeModel
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