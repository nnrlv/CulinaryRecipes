namespace CulinaryRecipes.Context.Entities;

/// <summary>
/// Represents an ingredient that can be used in recipes.
/// </summary>
public class Ingredient : BaseEntity
{
    /// <summary>
    /// The name of the ingredient.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The unit of measurement for the ingredient.
    /// </summary>
    public string UnitOfMeasurement { get; set; }

    /// <summary>
    /// The number of calories per unit of the ingredient.
    /// </summary>
    public float Calories { get; set; }

    /// <summary>
    /// The amount of proteins per unit of the ingredient.
    /// </summary>
    public float Proteins { get; set; }

    /// <summary>
    /// The amount of carbohydrates per unit of the ingredient.
    /// </summary>
    public float Carbohydrates { get; set; }

    /// <summary>
    /// The amount of fats per unit of the ingredient.
    /// </summary>
    public float Fats { get; set; }

    /// <summary>
    /// The recipes that use this ingredient.
    /// </summary>
    public virtual ICollection<IngredientInRecipe>? IngredientsInRecipe { get; set; }
}
