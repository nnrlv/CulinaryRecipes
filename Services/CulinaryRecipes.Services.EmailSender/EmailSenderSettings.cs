namespace CulinaryRecipes.Services.EmailSender;

/// <summary>
/// Settings for the email sender.
/// </summary>
public class EmailSenderSettings
{
    /// <summary>
    /// Email address.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Email password.
    /// </summary>
    public string Password { get; private set; }

    /// <summary>
    /// Email host.
    /// </summary>
    public string Host { get; private set; }

    /// <summary>
    /// Email port.
    /// </summary>
    /// </summary>
    public int Port { get; private set; }
}
