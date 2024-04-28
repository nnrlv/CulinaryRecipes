using AutoMapper;
using CulinaryRecipes.Common.Security;
using CulinaryRecipes.Services.Comments;
using CulinaryRecipes.Services.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CulinaryRecipes.Api.Controllers.Comment;

/// <summary>
/// Controller for managing comments.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class CommentController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IAppLogger logger;
    private readonly ICommentService commentService;

    /// <summary>
    /// Initializes a new instance of the CommentController class.
    /// </summary>
    /// <param name="mapper">The mapper service.</param>
    /// <param name="logger">The logger service.</param>
    /// <param name="commentService">The comment service.</param>
    public CommentController(IMapper mapper, IAppLogger logger, ICommentService commentService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.commentService = commentService;
    }

    /// <summary>
    /// Gets all comments for a recipe by ID.
    /// </summary>
    /// <param name="recipeId">The ID of the recipe.</param>
    /// <returns>A collection of comment responses.</returns>
    [HttpGet("recipe/{recipeId:Guid}")]
    [Authorize(Policy = AppScopes.CommentsRead)]
    public async Task<IEnumerable<CommentResponse>> GetAllByRecipeId([FromRoute] Guid recipeId)
    {
        var comments = await commentService.GetAllByRecipeId(recipeId);

        var result = mapper.Map<IEnumerable<CommentResponse>>(comments);

        return result;
    }

    /// <summary>
    /// Gets all comments for a recipe by ID with caching.
    /// </summary>
    /// <param name="recipeId">The ID of the recipe.</param>
    /// <returns>A collection of comment responses.</returns>
    [HttpGet("cached/recipe/{recipeId:Guid}")]
    [Authorize(Policy = AppScopes.CommentsRead)]
    public async Task<IEnumerable<CommentResponse>> GetAllByRecipeIdWithCaching([FromRoute] Guid recipeId)
    {
        var comments = await commentService.GetAllByRecipeIdWithCaching(recipeId);

        var result = mapper.Map<IEnumerable<CommentResponse>>(comments);

        return result;
    }

    [HttpGet("user/{userId:Guid}")]
    [Authorize(Policy = AppScopes.CommentsRead)]
    public async Task<IEnumerable<CommentResponse>> GetAllByUserId([FromRoute] Guid userId)
    {
        var comments = await commentService.GetAllByUserId(userId);

        var result = mapper.Map<IEnumerable<CommentResponse>>(comments);

        return result;
    }

    /// <summary>
    /// Gets all comments by user ID.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A collection of comment responses.</returns>
    [HttpGet("cached/user/{userId:Guid}")]
    [Authorize(Policy = AppScopes.CommentsRead)]
    public async Task<IEnumerable<CommentResponse>> GetAllByUserIdWithCaching([FromRoute] Guid userId)
    {
        var comments = await commentService.GetAllByUserIdWithCaching(userId);

        var result = mapper.Map<IEnumerable<CommentResponse>>(comments);

        return result;
    }

    /// <summary>
    /// Gets a comment by ID.
    /// </summary>
    /// <param name="id">The ID of the comment.</param>
    /// <returns>The comment response.</returns>
    [HttpGet("{id:Guid}")]
    [Authorize(Policy = AppScopes.CommentsRead)]
    [HttpGet("{id:Guid}")]
    [Authorize(Policy = AppScopes.CommentsRead)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await commentService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Gets a comment by ID with caching.
    /// </summary>
    /// <param name="id">The ID of the comment.</param>
    /// <returns>The comment response.</returns>
    [HttpGet("cached/{id:Guid}")]
    [Authorize(Policy = AppScopes.CommentsRead)]
    public async Task<IActionResult> GetByIdWithCaching([FromRoute] Guid id)
    {
        var result = await commentService.GetByIdWithCaching(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Creates a new comment.
    /// </summary>
    /// <param name="request">The request to create a comment.</param>
    /// <returns>The created comment response.</returns>
    [HttpPost("")]
    [Authorize(Policy = AppScopes.CommentsWrite)]
    public async Task<CommentResponse> Create([FromQuery] CreateCommentRequest request)
    {
        var comment = await commentService.Create(mapper.Map<CreateCommentModel>(request));

        var result = mapper.Map<CommentResponse>(comment);

        return result;
    }

    /// <summary>
    /// Deletes a comment by ID.
    /// </summary>
    /// <param name="id">The ID of the comment.</param>
    /// <returns>An IActionResult representing the result of the operation.</returns>
    [HttpDelete("{id:Guid}")]
    [Authorize(Policy = AppScopes.CommentsWrite)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await commentService.Delete(id);

        return Ok();
    }
}