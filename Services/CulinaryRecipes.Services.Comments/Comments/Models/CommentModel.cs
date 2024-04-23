using AutoMapper;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CulinaryRecipes.Services.Comments;

public class CommentModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public Guid RecipeId { get; set; }

    public string Text { get; set; }
}

public class CommentModelProfile : Profile
{
    public CommentModelProfile()
    {
        CreateMap<Comment, CommentModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.Recipe.Uid));
    }
}