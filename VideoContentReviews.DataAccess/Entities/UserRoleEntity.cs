namespace VideoContentReviews.DataAccess.Entities;

public class UserRoleEntity : BaseEntity
{
    public string Name { get; set; }
    
    public virtual ICollection<UserEntity> Users { get; set; }
}