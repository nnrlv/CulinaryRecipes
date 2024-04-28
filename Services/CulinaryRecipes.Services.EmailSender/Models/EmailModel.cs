namespace CulinaryRecipes.Services.EmailSender;

/// <summary>
/// Represents an email model for sending emails.
/// </summary>
public class EmailModel
{
    /// <summary>
    /// The email address.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// The email subject.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// The email message.
    /// </summary>
    public string Message { get; set; }
}