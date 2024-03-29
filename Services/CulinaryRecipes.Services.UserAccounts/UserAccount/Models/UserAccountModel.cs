﻿namespace CulinaryRecipes.Services.UserAccounts;

using AutoMapper;
using CulinaryRecipes.Context.Entities;

public class UserAccountModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
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
