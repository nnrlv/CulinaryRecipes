namespace CulinaryRecipes.Api.Controllers.UserAccounts;

using AutoMapper;
using CulinaryRecipes.Services.UserAccounts;

public class UserAccountResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class UserAccountResponseProfile : Profile
{
    public UserAccountResponseProfile()
    {
        CreateMap<UserAccountModel, UserAccountResponse>();
    }
}