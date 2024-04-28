namespace CulinaryRecipes.Services.Comments;

/// <summary>
/// Service for managing comments.
/// </summary>
public interface ICommentService
{
    /// <summary>
    /// Gets all comments for a given recipe ID.
    /// </summary>
    /// <param name="recipeId">The ID of the recipe.</param>
    /// <returns>The task result contains a collection of comment models.</returns>
    Task<IEnumerable<CommentModel>> GetAllByRecipeId(Guid recipeId);

    /// <summary>
    /// Gets all comments for a given recipe ID with caching.
    /// </summary>
    /// <param name="recipeId">The ID of the recipe.</param>
    /// <returns>The task result contains a collection of comment models.</returns>
    Task<IEnumerable<CommentModel>> GetAllByRecipeIdWithCaching(Guid recipeId);

    /// <summary>
    /// Gets all comments for a given user ID.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>The task result contains a collection of comment models.</returns>
    Task<IEnumerable<CommentModel>> GetAllByUserId(Guid userId);

    /// <summary>
    /// Gets all comments for a given user ID with caching.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>The task result contains a collection of comment models.</returns>
    Task<IEnumerable<CommentModel>> GetAllByUserIdWithCaching(Guid userId);

    /// <summary>
    /// Gets a comment by ID.
    /// </summary>
    /// <param name="id">The ID of the comment.</param>
    /// <returns>The task result contains the comment model.</returns>
    Task<CommentModel> GetById(Guid id);

    /// <summary>
    /// Gets a comment by ID with caching.
    /// </summary>
    /// <param name="id">The ID of the comment.</param>
    /// <returns>The task result contains the comment model.</returns>
    Task<CommentModel> GetByIdWithCaching(Guid id);

    /// <summary>
    /// Creates a new comment.
    /// </summary>
    /// <param name="model">The model for creating the comment.</param>
    Task<CommentModel> Create(CreateCommentModel model);

    /// <summary>
    /// Deletes a comment by ID.
    /// </summary>
    /// <param name="id">The ID of the comment to delete.</param>
    Task Delete(Guid id);
}