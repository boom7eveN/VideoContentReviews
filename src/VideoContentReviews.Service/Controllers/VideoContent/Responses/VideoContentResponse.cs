using VideoContentReviews.Service.Controllers.Genres.DTOs.Responses;

namespace VideoContentReviews.Service.Controllers.VideoContent.Responses;

public class VideoContentResponse
{
    public Guid Id { get; set; }
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
    
    public List<GenreResponse> Genres { get; set; } = new();
    
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
}