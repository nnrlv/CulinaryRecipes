namespace CulinaryRecipes.Context.Entities;

public class UserSubscription : BaseEntity
{
    public int SubscriberId { get; set; }
    //public User Subscriber { get; set; }

    public int AuthorId { get; set; }
    public User Author { get; set; }
}
