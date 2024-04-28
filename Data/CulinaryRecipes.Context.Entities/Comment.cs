using CulinaryRecipes.Context.Entities;

/// <summary>
/// Represents a comment made by a user on a recipe.
/// </summary>
public class Comment : BaseEntity
{
    /// <summary>
    /// The ID of the user who made the comment.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// The user who made the comment.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// The ID of the recipe the comment is for.
    /// </summary>
    public int RecipeId { get; set; }

    /// <summary>
    /// The recipe the comment is for.
    /// </summary>
    public Recipe Recipe { get; set; }

    /// <summary>
    /// The text of the comment.
    /// </summary>
    public string Text { get; set; }
}
