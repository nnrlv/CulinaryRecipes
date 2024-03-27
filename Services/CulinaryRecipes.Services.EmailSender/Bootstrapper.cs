namespace CulinaryRecipes.Services.EmailSender;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CulinaryRecipes.Settings;

public static class Bootstrapper
{
    public static IServiceCollection AddEmailSender(
        this IServiceCollection services,
        IConfiguration? configuration = null
        )
    {
        var settings = Settings.Load<EmailSenderSettings>("EmailSender", configuration);
        services.AddSingleton(settings);

        services.AddSingleton<IEmailSender, EmailSender>();

        return services;
    }
}