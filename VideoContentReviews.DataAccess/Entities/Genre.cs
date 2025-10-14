namespace VideoContentReviews.DataAccess.Entities;

public class Genre : BaseEntity
{
    public String Title { get; set; }
    
    public virtual ICollection<VideoContentGenre> VideoContentsGenres { get; set; }
}