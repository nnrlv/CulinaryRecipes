namespace CulinaryRecipes.Context.Entities;

public class Recipe : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }

    public string Name { get; set; }

    public float PreparationTime { get; set; }
    public float CookingTime { get; set; }

    public string Description { get; set; }
    public string Instructions { get; set; }

    public float Calories { get; set; }
    public float Proteins { get; set; }
    public float Carbohydrates { get; set; }
    public float Fats { get; set; }

    public virtual ICollection<IngredientInRecipe> IngredientsInRecipe { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
}
