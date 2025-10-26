using System.ComponentModel.DataAnnotations.Schema;
using VideoContentReviews.DataAccess.Entities.Primitives;

namespace VideoContentReviews.DataAccess.Entities;
[Table("Users")] 
public class UserEntity : BaseEntity
{
    public string Email { get; set; }

    public UserRole UserRole { get; set; }

    public virtual ICollection<ReviewEntity> Reviews { get; set; }
    public virtual ICollection<FavouriteEntity> Favourites { get; set; }
}