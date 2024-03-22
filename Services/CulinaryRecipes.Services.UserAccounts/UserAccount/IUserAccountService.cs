namespace CulinaryRecipes.Services.UserAccounts;

public interface IUserAccountService
{
    Task<bool> IsEmpty();

    Task<UserAccountModel> Create(CreateUserAccountModel model);

    Task<IEnumerable<UserAccountModel>> GetAll();

    Task<UserAccountModel> GetById(Guid id);
}
