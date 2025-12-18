namespace VideoContentReviews.Service.Controllers.VideoContent.Requests;

public class UpdateVideoContentRequest
{
    public string? Name { get; set; }
    public int? YearOfRelease { get; set; }
    public string? Description { get; set; }
}