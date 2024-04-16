using AutoMapper;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CulinaryRecipes.Services.Recipes;

public class CategoryInRecipeModel
{
    public Guid RecipeId { get; set; }
    public Guid CategoryId { get; set; }
}


public class CategoryInRecipeModelProfile : Profile
{
    public CategoryInRecipeModelProfile()
    {
        CreateMap<RecipeInCategory, CategoryInRecipeModel>()
            .BeforeMap<CategoryInRecipeModelActions>()
            .ForMember(dest => dest.RecipeId, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
            ;
    }
}

public class CategoryInRecipeModelActions : IMappingAction<RecipeInCategory, CategoryInRecipeModel>
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;

    public CategoryInRecipeModelActions(IDbContextFactory<MainDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public void Process(RecipeInCategory source, CategoryInRecipeModel destination, ResolutionContext context)
    {
        using var db = contextFactory.CreateDbContext();

        var recipe = db.Recipes.FirstOrDefault(x => x.Id == source.RecipeId);

        var category = db.Categories.FirstOrDefault(x => x.Id == source.CategoryId);

        if (recipe != null && category != null)
        {
            destination.RecipeId = recipe.Uid;
            destination.CategoryId = category.Uid;
        }
    }
}