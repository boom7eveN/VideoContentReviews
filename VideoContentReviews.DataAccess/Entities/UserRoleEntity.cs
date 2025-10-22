using System.ComponentModel.DataAnnotations.Schema;

namespace VideoContentReviews.DataAccess.Entities;
[Table("UserRoles")] 
public class UserRoleEntity : BaseEntity
{
    public string Name { get; set; }
    
    public virtual ICollection<UserEntity> Users { get; set; }
}