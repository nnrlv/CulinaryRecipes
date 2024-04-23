using AutoMapper;
using CulinaryRecipes.Services.Comments;

namespace CulinaryRecipes.Api.Controllers.Comment;

public class CommentResponse
{
    public Guid Id { get; set; }

    public string Text { get; set; }

    public Guid UserId { get; set; }

    public Guid RecipeId { get; set; }
}

public class CommentResponseProfile : Profile
{
    public CommentResponseProfile()
    {
        CreateMap<CommentModel, CommentResponse>();
    }
}