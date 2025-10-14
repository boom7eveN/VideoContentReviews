namespace VideoContentReviews.DataAccess.Entities;

public class VideoContentGenre
{
    public Guid VideoContentId { get; set; }
    public VideoContent VideoContent { get; set; }

    public Guid GenreId { get; set; }
    public Genre Genre { get; set; }

    public DateTime AddedTime { get; set; }
}