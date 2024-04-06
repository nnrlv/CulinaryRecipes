using AutoMapper;
using CulinaryRecipes.Context.Entities;
using FluentValidation;

namespace CulinaryRecipes.Services.Categories;

public class UpdateCategoryModel
{
    public string Name { get; set; }
}

public class UpdateCategoryModelProfile : Profile
{
    public UpdateCategoryModelProfile()
    {
        CreateMap<UpdateCategoryModel, Category>();
    }
}

public class UpdateCategoryModelValidator : AbstractValidator<UpdateCategoryModel>
{
    public UpdateCategoryModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MinimumLength(2).WithMessage("Minimum length is 2")
            .MaximumLength(25).WithMessage("Maximum length is 25");
    }
}