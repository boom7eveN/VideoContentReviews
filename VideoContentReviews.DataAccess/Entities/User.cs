namespace VideoContentReviews.DataAccess.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public Guid UserRoleId { get; set; }
    public UserRole UserRole { get; set; }

    public virtual ICollection<Review> Reviews { get; set; }
    public virtual ICollection<Favourite> Favourites { get; set; }
}