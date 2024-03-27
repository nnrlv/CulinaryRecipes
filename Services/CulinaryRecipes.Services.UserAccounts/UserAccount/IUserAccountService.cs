using Microsoft.AspNetCore.Mvc;

namespace CulinaryRecipes.Services.UserAccounts;

public interface IUserAccountService
{
    Task<bool> IsEmpty();

    Task<UserAccountModel> Create(CreateUserAccountModel model);

    Task RequestEmailConfirmation(string email);

    Task ConfirmEmail(string token, string email);

    Task<IEnumerable<UserAccountModel>> GetAll();

    Task<UserAccountModel> GetById(Guid id);
}
