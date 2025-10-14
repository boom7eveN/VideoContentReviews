namespace VideoContentReviews.DataAccess.Entities;

public class UserRole : BaseEntity
{
    public string Name { get; set; }
    
    public virtual ICollection<User> Users { get; set; }
}