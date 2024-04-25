namespace CulinaryRecipes.Context.Entities;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

public class User : IdentityUser<Guid>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EntryId { get; set; }

    public string Name { get; set; }
    public UserStatus Status { get; set; }

    public virtual ICollection<Recipe>? Recipes { get; set; }
    public virtual ICollection<Comment>? Comments { get; set; }
    public virtual ICollection<UserSubscription>? UserSubscriptions { get; set; }
    public virtual ICollection<RecipeSubscription>? RecipeSubscriptions { get; set; }

}
