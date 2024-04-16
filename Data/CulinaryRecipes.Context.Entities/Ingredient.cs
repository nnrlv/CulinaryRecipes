namespace CulinaryRecipes.Context.Entities;

public class Ingredient : BaseEntity
{
    public string Name { get; set; }
    public string UnitOfMeasurement { get; set; }

    public float Calories { get; set; }
    public float Proteins { get; set; }
    public float Carbohydrates { get; set; }
    public float Fats { get; set; }

    public virtual ICollection<IngredientInRecipe>? IngredientsInRecipe { get; set; }
}
