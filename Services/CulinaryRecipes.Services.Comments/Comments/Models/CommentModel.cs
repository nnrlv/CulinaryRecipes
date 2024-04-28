using AutoMapper;
using CulinaryRecipes.Context;
using CulinaryRecipes.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CulinaryRecipes.Services.Comments;

/// <summary>
/// Represents a model for a comment.
/// </summary>
public class CommentModel
{
    /// <summary>
    /// The ID of the comment.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The ID of the user who posted the comment.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The ID of the recipe the comment belongs to.
    /// </summary>
    public Guid RecipeId { get; set; }

    /// <summary>
    /// The text of the comment.
    /// </summary>
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