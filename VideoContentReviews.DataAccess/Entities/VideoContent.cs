namespace VideoContentReviews.DataAccess.Entities;

public class VideoContent : BaseEntity
{
    public String Name { get; set; }
    public int YearOfRelease { get; set; }
    public String Description { get; set; }
    public double UserAverageRating { get; set; }
    
    public Guid TypeOfContentId { get; set; }
    public TypeOfContent TypeOfContent { get; set; }
    
    public Guid DirectorId { get; set; }
    public Director Director { get; set; }
    
    public Guid ImageId { get; set; }
    public Image Image { get; set; }
    
    public virtual ICollection<Review> Reviews { get; set; }
    public virtual ICollection<Favourite> Favourites { get; set; }
    public virtual ICollection<VideoContentGenre> VideoContentsGenres { get; set; }
}