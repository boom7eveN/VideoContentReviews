namespace VideoContentReviews.DataAccess.Entities;

public class VideoContentGenreEntity : BaseEntity
{
    public Guid VideoContentId { get; set; }
    public VideoContentEntity VideoContentEntity { get; set; }

    public Guid GenreId { get; set; }
    public GenreEntity GenreEntity { get; set; }

    public DateTime AddedTime { get; set; }
}