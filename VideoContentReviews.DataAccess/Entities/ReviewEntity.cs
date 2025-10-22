using System.ComponentModel.DataAnnotations.Schema;

namespace VideoContentReviews.DataAccess.Entities;
[Table("Reviews")] 
public class ReviewEntity : BaseEntity
{
    public int Rate{get; set;}
    public String Text{get; set;}
    
    public Guid UserId { get; set; }
    public UserEntity UserEntity { get; set; }
    
    public Guid VideoContentId { get; set; }
    public VideoContentEntity VideoContentEntity { get; set; }
    
}