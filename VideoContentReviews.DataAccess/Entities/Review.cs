namespace VideoContentReviews.DataAccess.Entities;

public class Review : BaseEntity
{
    public int Rate{get; set;}
    public String Text{get; set;}
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid VideoContentId { get; set; }
    public VideoContent VideoContent { get; set; }
    
}