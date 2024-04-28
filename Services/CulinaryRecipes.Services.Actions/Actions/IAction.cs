namespace CulinaryRecipes.Services.Actions;

using CulinaryRecipes.Services.EmailSender;
using System.Threading.Tasks;

/// <summary>
/// Represents an action to be performed, such as sending emails.
/// </summary>
public interface IAction
{
    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="model">The email model.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendEmail(EmailModel model);

    /// <summary>
    /// Sends a new recipe notification based on user subscription information.
    /// </summary>
    /// <param name="model">The email model.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendNewRecipeFromUserSubscriptionInfo(EmailModel model);

    /// <summary>
    /// Sends a new comment notification based on recipe subscription information.
    /// </summary>
    /// <param name="model">The email model.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendNewCommentFromRecipeSubscriptionInfo(EmailModel model);
}