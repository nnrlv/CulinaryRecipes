using AutoMapper;
using CulinaryRecipes.Services.Comments;

namespace CulinaryRecipes.Api.Controllers.Comment;

public class CreateCommentRequest
{
    public string Text { get; set; }

    public Guid UserId { get; set; }

    public Guid RecipeId { get; set; }
}

public class CreateCommentRequestProfile : Profile
{
    public CreateCommentRequestProfile()
    {
        CreateMap<CreateCommentRequest, CreateCommentModel>();
    }
}