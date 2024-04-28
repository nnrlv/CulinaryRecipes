using AutoMapper;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CulinaryRecipes.Services.Ingredients;

/// <summary>
/// Represents a model for updating an ingredient.
/// </summary>
public class UpdateIngredientModel
{
    /// <summary>
    /// The name of the ingredient.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The unit of measurement for the ingredient.
    /// </summary>
    public string UnitOfMeasurement { get; set; }
}

public class UpdateIngredientModelProfile : Profile
{
    public UpdateIngredientModelProfile()
    {
        CreateMap<UpdateIngredientModel, Ingredient>();
    }
}

public class UpdateIngredientModelValidator : AbstractValidator<UpdateIngredientModel>
{
    public UpdateIngredientModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ingredient name is required")
            .MinimumLength(4).WithMessage("Minimum length is 4")
            .MaximumLength(25).WithMessage("Maximum length is 25");

        RuleFor(x => x.UnitOfMeasurement)
            .NotEmpty().WithMessage("Ingredient unit of measurement is required")
            .MinimumLength(1).WithMessage("Minimum length is 1")
            .MaximumLength(4).WithMessage("Maximum length is 4");
    }
}
