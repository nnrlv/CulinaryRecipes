namespace CulinaryRecipes.Services.UserAccounts;

/// <summary>
/// Interface for a service for managing user accounts.
/// </summary>
public interface IUserAccountService
{
    /// <summary>
    /// Checks if the user account database is empty.
    /// </summary>
    Task<bool> IsEmpty();

    /// <summary>
    /// Creates a new user account based on the specified model.
    /// </summary>
    Task<UserAccountModel> Create(CreateUserAccountModel model);

    /// <summary>
    /// Sends a request for email confirmation to the specified email address.
    /// </summary>
    Task RequestEmailConfirmation(string email);

    /// <summary>
    /// Confirms the email address using the specified confirmation token.
    /// </summary>
    Task ConfirmEmail(string token, string email);

    /// <summary>
    /// Gets all user accounts.
    /// </summary>
    Task<IEnumerable<UserAccountModel>> GetAll();

    /// <summary>
    /// Gets a user account by its ID.
    /// </summary>
    Task<UserAccountModel> GetById(Guid id);
}
