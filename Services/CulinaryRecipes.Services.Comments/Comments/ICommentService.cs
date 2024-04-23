namespace CulinaryRecipes.Services.Comments;

public interface ICommentService
{
    Task<IEnumerable<CommentModel>> GetAllByRecipeId(Guid recipeId);

    Task<IEnumerable<CommentModel>> GetAllByRecipeIdWithCaching(Guid recipeId);

    Task<IEnumerable<CommentModel>> GetAllByUserId(Guid userId);

    Task<IEnumerable<CommentModel>> GetAllByUserIdWithCaching(Guid userId);

    Task<CommentModel> GetById(Guid id);

    Task<CommentModel> GetByIdWithCaching(Guid id);

    Task<CommentModel> Create(CreateCommentModel model);

    Task Delete(Guid id);
}