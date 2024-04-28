namespace CulinaryRecipes.Services.UserAccounts;

using AutoMapper;
using CulinaryRecipes.Context.Entities;

/// <summary>
/// Represents a user account model.
/// </summary>
public class UserAccountModel
{
    /// <summary>
    /// The ID of the user account.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the user.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The email address of the user.
    /// </summary>
    public string Email { get; set; }
}

public class UserAccountModelProfile : Profile
{
    public UserAccountModelProfile()
    {
        CreateMap<User, UserAccountModel>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
            ;
    }
}
