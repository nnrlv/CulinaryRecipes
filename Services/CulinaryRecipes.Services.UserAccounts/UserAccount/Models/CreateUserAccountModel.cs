namespace CulinaryRecipes.Services.UserAccounts;

using FluentValidation;

/// <summary>
/// Represents a model for creating a user account.
/// </summary>
public class CreateUserAccountModel
{
    /// <summary>
    /// The name of the user.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The email address of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// The password of the user.
    /// </summary>
    public string Password { get; set; }
}

public class CreateUserAccountModelValidator : AbstractValidator<CreateUserAccountModel>
{
    public CreateUserAccountModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("User name is required.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MaximumLength(50).WithMessage("Password is long.");
    }
}