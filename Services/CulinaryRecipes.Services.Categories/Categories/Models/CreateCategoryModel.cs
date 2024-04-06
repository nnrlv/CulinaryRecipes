using AutoMapper;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CulinaryRecipes.Services.Categories;

public class CreateCategoryModel
{
    public string Name { get; set; }
    public int? CategoryId { get; set; }
}

public class CreateCategoryModelProfile : Profile
{
    public CreateCategoryModelProfile()
    {
        CreateMap<CreateCategoryModel, Category>()
            .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
            .AfterMap<CreateCategoryModelActions>();
    }
}

public class CreateCategoryModelActions : IMappingAction<CreateCategoryModel, Category>
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;

    public CreateCategoryModelActions(IDbContextFactory<MainDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public void Process(CreateCategoryModel source, Category destination, ResolutionContext context)
    {
        if (source.CategoryId is not null)
        {
            using var db = contextFactory.CreateDbContext();

            var parentCategory = db.Categories.FirstOrDefault(x => x.Id == source.CategoryId);

            if (parentCategory is not null)
            {
                destination.CategoryId = parentCategory.Id;
            }
            else
            {
                throw new InvalidOperationException("Parent category not found.");
            }
        }
    }
}

public class CreateCategoryModelValidator : AbstractValidator<CreateCategoryModel>
{
    public CreateCategoryModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MinimumLength(2).WithMessage("Minimum length is 2")
            .MaximumLength(25).WithMessage("Maximum length is 25");
    }
}