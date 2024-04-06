using AutoMapper;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CulinaryRecipes.Services.Categories;

public class CategoryModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? CategoryId { get; set; }
}

public class CategoryModelProfile : Profile
{
    public CategoryModelProfile()
    {
        CreateMap<Category, CategoryModel>()
            .BeforeMap<CategoryModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}

public class CategoryModelActions : IMappingAction<Category, CategoryModel>
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;

    public CategoryModelActions(IDbContextFactory<MainDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public void Process(Category source, CategoryModel destination, ResolutionContext context)
    {
        using var db = contextFactory.CreateDbContext();

        var category = db.Categories.FirstOrDefault(x => x.Id == source.Id);

        destination.Id = category.Uid;
    }
}