using AutoMapper;
using CulinaryRecipes.Context.Entities;
using CulinaryRecipes.Common.Validator;
using FluentValidation;

namespace CulinaryRecipes.Services.Ingredients;

/// <summary>
/// Represents a model for creating an ingredient.
/// </summary>
public class CreateIngredientModel
{
    /// <summary>
    /// The name of the ingredient.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The unit of measurement for the ingredient.
    /// </summary>
    public string UnitOfMeasurement { get; set; }

    /// <summary>
    /// The number of calories per unit of the ingredient.
    /// </summary>
    public float Calories { get; set; }

    /// <summary>
    /// The amount of proteins per unit of the ingredient.
    /// </summary>
    public float Proteins { get; set; }

    /// <summary>
    /// The amount of carbohydrates per unit of the ingredient.
    /// </summary>
    public float Carbohydrates { get; set; }

    /// <summary>
    /// The amount of fats per unit of the ingredient.
    /// </summary>
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