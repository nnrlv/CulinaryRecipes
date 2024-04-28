using AutoMapper;
using CulinaryRecipes.Context.Entities;
using FluentValidation;

namespace CulinaryRecipes.Services.Recipes;

/// <summary>
/// Represents a model for updating a recipe.
/// </summary>
public class UpdateRecipeModel
{
    /// <summary>
    /// The new name of the recipe.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The new preparation time of the recipe.
    /// </summary>
    public float PreparationTime { get; set; }

    /// <summary>
    /// The new cooking time of the recipe.
    /// </summary>
    public float CookingTime { get; set; }

    /// <summary>
    /// The new description of the recipe.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The new instructions for preparing the recipe.
    /// </summary>
    public string Instructions { get; set; }

    /// <summary>
    /// The new number of calories in the recipe.
    /// </summary>
    public float Calories { get; set; }

    /// <summary>
    /// The new amount of proteins in the recipe.
    /// </summary>
    public float Proteins { get; set; }

    /// <summary>
    /// The new amount of carbohydrates in the recipe.
    /// </summary>
    public float Carbohydrates { get; set; }

    /// <summary>
    /// The new amount of fats in the recipe.
    /// </summary>
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