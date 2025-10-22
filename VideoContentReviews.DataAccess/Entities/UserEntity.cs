namespace VideoContentReviews.DataAccess.Entities;

public class UserEntity : BaseEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public Guid UserRoleId { get; set; }
    public UserRoleEntity UserRoleEntity { get; set; }

    public virtual ICollection<ReviewEntity> Reviews { get; set; }
    public virtual ICollection<FavouriteEntity> Favourites { get; set; }
}