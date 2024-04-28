namespace CulinaryRecipes.Context.Entities;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Index("Uid", IsUnique = true)]
/// <summary>
/// Represents a base entity for the database.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// The unique identifier for the entity.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    /// <summary>
    /// The globally unique identifier (GUID) for the entity.
    /// </summary>
    [Required]
    public virtual Guid Uid { get; set; } = Guid.NewGuid();
}
