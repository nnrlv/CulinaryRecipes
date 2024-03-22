namespace CulinaryRecipes.Api.Controllers.UserAccounts;

using AutoMapper;
using CulinaryRecipes.Services.UserAccounts;

public class CreateUserAccountRequest
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; }
}

public class CreateUserAccountRequestProfile : Profile
{
    public CreateUserAccountRequestProfile()
    {
        CreateMap<CreateUserAccountRequest, CreateUserAccountModel>();
    }
}