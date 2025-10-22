namespace VideoContentReviews.DataAccess.Entities;

public class FavouriteEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public UserEntity UserEntity { get; set; }
    
    public Guid VideoContentId { get; set; }
    public VideoContentEntity VideoContentEntity { get; set; }
}