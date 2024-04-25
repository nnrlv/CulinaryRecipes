namespace CulinaryRecipes.Context.Entities;

public class Recipe : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public string Name { get; set; }

    public float PreparationTime { get; set; }
    public float CookingTime { get; set; }

    public string Description { get; set; }
    public string Instructions { get; set; }

    public float Calories { get; set; }
    public float Proteins { get; set; }
    public float Carbohydrates { get; set; }
    public float Fats { get; set; }

    public virtual ICollection<IngredientInRecipe>? IngredientsInRecipe { get; set; }
    public virtual ICollection<RecipeInCategory>? RecipesInCategories { get; set; }
    public virtual ICollection<Comment>? Comments { get; set; }
    public virtual ICollection<RecipeSubscription>? RecipeSubscriptions { get; set; }
}
