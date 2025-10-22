using System.ComponentModel.DataAnnotations.Schema;

namespace VideoContentReviews.DataAccess.Entities;
[Table("VideoContent")] 
public class VideoContentEntity : BaseEntity
{
    public String Name { get; set; }
    public int YearOfRelease { get; set; }
    public String Description { get; set; }
    public double UserAverageRating { get; set; }
    
    public Guid TypeOfContentId { get; set; }
    public TypeOfContentEntity TypeOfContentEntity { get; set; }
    
    public Guid DirectorId { get; set; }
    public DirectorEntity DirectorEntity { get; set; }
    
    public Guid ImageId { get; set; }
    public ImageEntity ImageEntity { get; set; }
    
    public virtual ICollection<ReviewEntity> Reviews { get; set; }
    public virtual ICollection<FavouriteEntity> Favourites { get; set; }
    public virtual ICollection<VideoContentGenreEntity> VideoContentsGenres { get; set; }
}