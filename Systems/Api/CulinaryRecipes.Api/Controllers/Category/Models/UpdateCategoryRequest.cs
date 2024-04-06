using AutoMapper;
using CulinaryRecipes.Services.Categories;

namespace CulinaryRecipes.Api.Controllers.Category;

public class UpdateCategoryRequest
{
    public string Name { get; set; }
}

public class UpdateCategoryRequestProfile : Profile
{
    public UpdateCategoryRequestProfile()
    {
        CreateMap<UpdateCategoryRequest, UpdateCategoryModel>();
    }
}