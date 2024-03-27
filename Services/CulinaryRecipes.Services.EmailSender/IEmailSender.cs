namespace CulinaryRecipes.Services.EmailSender;

public interface IEmailSender
{
    Task SendEmailAsync(EmailModel email);
}
