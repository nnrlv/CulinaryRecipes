namespace CulinaryRecipes.Services.EmailSender;

public class EmailSenderSettings
{
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Host { get; private set; }
    public int Port { get; private set; }
}
