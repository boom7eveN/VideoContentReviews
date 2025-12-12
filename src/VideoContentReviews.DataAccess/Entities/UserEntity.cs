using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using VideoContentReviews.DataAccess.Entities.Primitives;

namespace VideoContentReviews.DataAccess.Entities;

[Table("Users")]
public class UserEntity : IdentityUser<int>, IBaseEntity
{
    public virtual ICollection<ReviewEntity> Reviews { get; set; }
    public virtual ICollection<FavouriteEntity> Favourites { get; set; }
    public Guid ExternalId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
    public UserRole Role { get; set; }
}

public class UserRoleEntity : IdentityRole<int>
{
}