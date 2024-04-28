namespace CulinaryRecipes.Context.Entities;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents a user in the system.
/// </summary>
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// The ID of the user's entry.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EntryId { get; set; }

    /// <summary>
    /// The name of the user.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The status of the user (active/blocked).
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// The recipes authored by the user.
    /// </summary>
    public virtual ICollection<Recipe>? Recipes { get; set; }

    /// <summary>
    /// The comments authored by the user.
    /// </summary>
    public virtual ICollection<Comment>? Comments { get; set; }

    /// <summary>
    /// The authors that user subscribed.
    /// </summary>
    public virtual ICollection<UserSubscription>? UserSubscriptions { get; set; }

    /// <summary>
    /// The recipes that user subscribed.
    /// </summary>
    public virtual ICollection<RecipeSubscription>? RecipeSubscriptions { get; set; }

}
