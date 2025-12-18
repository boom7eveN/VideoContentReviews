using VideoContentReviews.BL.Genres.Entities;

namespace VideoContentReviews.BL.VideoContent.Entities;

public class VideoContentModel
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    public string Name { get; set; }
    public int YearOfRelease { get; set; }
    public string Description { get; set; }
    public double UserAverageRating { get; set; }
    
    public Guid TypeOfContentId { get; set; }
    public string TypeOfContentName { get; set; }
    
    public Guid DirectorId { get; set; }
    public string DirectorFullName { get; set; }
    
    public Guid ImageId { get; set; }
    public string ImageFileName { get; set; }
    
    public List<GenreModel> Genres { get; set; } = new();
    
    
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
}