namespace VideoContentReviews.BL.Genres.Entities;

public class GenreModel
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    public string Title { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
}