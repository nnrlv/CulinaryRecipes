namespace CulinaryRecipes.Context.Entities;

/// <summary>
/// Represents a recipe entity.
/// </summary>
public class Recipe : BaseEntity
{
    /// <summary>
    /// The ID of the user who created the recipe.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// The user who created the recipe.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// The name of the recipe.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The preparation time of the recipe.
    /// </summary>
    public float PreparationTime { get; set; }

    /// <summary>
    /// The cooking time of the recipe.
    /// </summary>
    public float CookingTime { get; set; }

    /// <summary>
    /// The description of the recipe.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The instructions for preparing the recipe.
    /// </summary>
    public string Instructions { get; set; }

    /// <summary>
    /// The number of calories in the recipe.
    /// </summary>
    public float Calories { get; set; }

    /// <summary>
    /// The amount of proteins in the recipe.
    /// </summary>
    public float Proteins { get; set; }

    /// <summary>
    /// The amount of carbohydrates in the recipe.
    /// </summary>
    public float Carbohydrates { get; set; }

    /// <summary>
    /// The amount of fats in the recipe.
    /// </summary>
    public float Fats { get; set; }

    /// <summary>
    /// The ingredients used in the recipe.
    /// </summary>
    public virtual ICollection<IngredientInRecipe>? IngredientsInRecipe { get; set; }

    /// <summary>
    /// The categories recipe belongs to.
    /// </summary>
    public virtual ICollection<RecipeInCategory>? RecipesInCategories { get; set; }

    /// <summary>
    /// The comments written under this recipe.
    /// </summary>
    public virtual ICollection<Comment>? Comments { get; set; }

    /// <summary>
    /// The subscriptions to this recipe.
    /// </summary>
    public virtual ICollection<RecipeSubscription>? RecipeSubscriptions { get; set; }
}
