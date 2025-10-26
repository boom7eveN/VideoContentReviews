using System.ComponentModel.DataAnnotations.Schema;

namespace VideoContentReviews.DataAccess.Entities;
[Table("Favourite")] 
public class FavouriteEntity : BaseEntity
{
    public int UserId { get; set; }
    public UserEntity UserEntity { get; set; }
    
    public int VideoContentId { get; set; }
    public VideoContentEntity VideoContentEntity { get; set; }
}