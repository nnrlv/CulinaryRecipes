namespace CulinaryRecipes.Services.EmailSender;

using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    private readonly EmailSenderSettings settings;

    public EmailSender(EmailSenderSettings settings)
    {
        this.settings = settings;
    }

    public async Task SendEmailAsync(EmailModel model)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress("CulinaryRecipes", settings.Email));
        email.To.Add(new MailboxAddress("", model.Email));
        email.Sender = MailboxAddress.Parse(settings.Email);
        email.Subject = model.Subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = model.Message
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(settings.Host, settings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(settings.Email, settings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}