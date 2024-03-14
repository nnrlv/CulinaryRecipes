using AutoMapper;
using CulinaryRecipes.Context.Entities;
using CulinaryRecipes.Common.Validator;
using FluentValidation;

namespace CulinaryRecipes.Services.Ingredients;

public class CreateIngredientModel
{
    public string Name { get; set; }
    public string UnitOfMeasurement { get; set; }

    public float Calories { get; set; }
    public float Proteins { get; set; }
    public float Carbohydrates { get; set; }
    public float Fats { get; set; }
}

public class CreateIngredientModelProfile : Profile
{
    public CreateIngredientModelProfile()
    {
        CreateMap<CreateIngredientModel, Ingredient>();
    }
}

public class CreateIngredientModelValidator : AbstractValidator<CreateIngredientModel>
{
    public CreateIngredientModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ingredient name is required")
            .MinimumLength(4).WithMessage("Minimum length is 4")
            .MaximumLength(25).WithMessage("Maximum length is 25");

        RuleFor(x => x.UnitOfMeasurement)
            .NotEmpty().WithMessage("Ingredient unit of measurement is required")
            .MinimumLength(1).WithMessage("Minimum length is 1")
            .MaximumLength(4).WithMessage("Maximum length is 4");

        RuleFor(x => x.Calories)
            .GreaterThanOrEqualTo(0.0f).WithMessage("Number of calories must be positive");

        RuleFor(x => x.Proteins)
            .GreaterThanOrEqualTo(0.0f).WithMessage("Number of proteins must be positive");

        RuleFor(x => x.Carbohydrates)
            .GreaterThanOrEqualTo(0.0f).WithMessage("Number of carbohydrates must be positive");

        RuleFor(x => x.Fats)
            .GreaterThanOrEqualTo(0.0f).WithMessage("Number of fats must be positive");
    }
}