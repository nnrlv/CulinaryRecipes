using AutoMapper;
using CulinaryRecipes.Context.Entities;
using FluentValidation;

namespace CulinaryRecipes.Services.Recipes;

/// <summary>
/// Represents a model for creating a recipe.
/// </summary>
public class CreateRecipeModel
{
    /// <summary>
    /// The name of the recipe.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The preparation time of the recipe.
    /// </summary>
    public float PreparationTime { get; set; }

    /// <summary>
    /// The cooking time of the recipe.
    /// </summary>
    public float CookingTime { get; set; }

    /// <summary>
    /// The description of the recipe.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The instructions for preparing the recipe.
    /// </summary>
    public string Instructions { get; set; }

    /// <summary>
    /// The number of calories in the recipe.
    /// </summary>
    public float Calories { get; set; }

    /// <summary>
    /// The amount of proteins in the recipe.
    /// </summary>
    public float Proteins { get; set; }

    /// <summary>
    /// The amount of carbohydrates in the recipe.
    /// </summary>
    public float Carbohydrates { get; set; }

    /// <summary>
    /// The amount of fats in the recipe.
    /// </summary>
    public float Fats { get; set; }
}

public class CreateRecipeModelProfile : Profile
{
    public CreateRecipeModelProfile()
    {
        CreateMap<CreateRecipeModel, Recipe>();
    }
}

public class CreateRecipeModelValidator : AbstractValidator<CreateRecipeModel>
{
    public CreateRecipeModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Recipe name is required")
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