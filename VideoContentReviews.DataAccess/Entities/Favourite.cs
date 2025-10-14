namespace VideoContentReviews.DataAccess.Entities;

public class Favourite : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid VideoContentId { get; set; }
    public VideoContent VideoContent { get; set; }
}