namespace VideoContentReviews.DataAccess.Entities;

public class GenreEntity : BaseEntity
{
    public String Title { get; set; }
    
    public virtual ICollection<VideoContentGenreEntity> VideoContentsGenres { get; set; }
}