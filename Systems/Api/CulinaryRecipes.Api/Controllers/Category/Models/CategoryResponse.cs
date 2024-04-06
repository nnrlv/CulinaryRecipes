using AutoMapper;
using CulinaryRecipes.Services.Categories;

namespace CulinaryRecipes.Api.Controllers.Category;

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? CategoryId { get; set; }
}

public class CategoryResponseProfile : Profile
{
    public CategoryResponseProfile()
    {
        CreateMap<CategoryModel, CategoryResponse>();
    }
}