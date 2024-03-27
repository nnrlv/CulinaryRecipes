namespace CulinaryRecipes.Services.Actions;

using System.Threading.Tasks;
using CulinaryRecipes.Services.EmailSender;

public interface IAction
{
    Task SendEmail(EmailModel model);
}
