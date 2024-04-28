namespace CulinaryRecipes.Services.EmailSender;

/// <summary>
/// Represents an interface for sending emails.
/// </summary>
public interface IEmailSender
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="email">The email model.</param>
    Task SendEmailAsync(EmailModel email);
}

