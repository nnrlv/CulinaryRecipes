using AutoMapper;
using CulinaryRecipes.Context.Entities;
using FluentValidation;

namespace CulinaryRecipes.Services.Recipes;

public class UpdateRecipeModel
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

public class UpdateRecipeModelProfile : Profile
{
    public UpdateRecipeModelProfile()
    {
        CreateMap<UpdateRecipeModel, Recipe>();
    }
}

public class UpdateRecipeModelValidator : AbstractValidator<UpdateRecipeModel>
{
    public UpdateRecipeModelValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(4).WithMessage("Minimum length is 4")
            .MaximumLength(25).WithMessage("Maximum length is 25");

        RuleFor(x => x.Calories)
            .GreaterThanOrEqualTo(0.0f).WithMessage("Number of calories must be positive");

        RuleFor(x => x.PreparationTime)
            .GreaterThanOrEqualTo(0.0f).WithMessage("Number of proteins must be positive");

        RuleFor(x => x.CookingTime)
            .GreaterThanOrEqualTo(0.0f).WithMessage("Number of calories must be positive");

        RuleFor(x => x.Proteins)
            .GreaterThanOrEqualTo(0.0f).WithMessage("Number of proteins must be positive");

        RuleFor(x => x.Carbohydrates)
            .GreaterThanOrEqualTo(0.0f).WithMessage("Number of carbohydrates must be positive");

        RuleFor(x => x.Fats)
            .GreaterThanOrEqualTo(0.0f).WithMessage("Number of fats must be positive");
    }
}