using AutoMapper;
using CulinaryRecipes.Services.Comments;
using CulinaryRecipes.Services.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CulinaryRecipes.Api.Controllers.Comment;


[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class CommentController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IAppLogger logger;
    private readonly ICommentService commentService;

    public CommentController(IMapper mapper, IAppLogger logger, ICommentService commentService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.commentService = commentService;
    }

    [HttpGet("recipe/{recipeId:Guid}")]
    public async Task<IEnumerable<CommentResponse>> GetAllByRecipeId([FromRoute] Guid recipeId)
    {
        var comments = await commentService.GetAllByRecipeId(recipeId);

        var result = mapper.Map<IEnumerable<CommentResponse>>(comments);

        return result;
    }

    [HttpGet("cached/recipe/{recipeId:Guid}")]
    public async Task<IEnumerable<CommentResponse>> GetAllByRecipeIdWithCaching([FromRoute] Guid recipeId)
    {
        var comments = await commentService.GetAllByRecipeIdWithCaching(recipeId);

        var result = mapper.Map<IEnumerable<CommentResponse>>(comments);

        return result;
    }

    [HttpGet("user/{userId:Guid}")]
    public async Task<IEnumerable<CommentResponse>> GetAllByUserId([FromRoute] Guid userId)
    {
        var comments = await commentService.GetAllByUserId(userId);

        var result = mapper.Map<IEnumerable<CommentResponse>>(comments);

        return result;
    }

    [HttpGet("cached/user/{userId:Guid}")]
    public async Task<IEnumerable<CommentResponse>> GetAllByUserIdWithCaching([FromRoute] Guid userId)
    {
        var comments = await commentService.GetAllByUserIdWithCaching(userId);

        var result = mapper.Map<IEnumerable<CommentResponse>>(comments);

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await commentService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("cached/{id:Guid}")]
    public async Task<IActionResult> GetByIdWithCaching([FromRoute] Guid id)
    {
        var result = await commentService.GetByIdWithCaching(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<CommentResponse> Create([FromQuery] CreateCommentRequest request)
    {
        var comment = await commentService.Create(mapper.Map<CreateCommentModel>(request));

        var result = mapper.Map<CommentResponse>(comment);

        return result;
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await commentService.Delete(id);

        return Ok();
    }
}