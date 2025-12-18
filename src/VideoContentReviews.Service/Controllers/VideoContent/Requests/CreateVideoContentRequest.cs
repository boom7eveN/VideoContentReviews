namespace VideoContentReviews.Service.Controllers.VideoContent.Requests;

public class CreateVideoContentRequest
{
    public string Name { get; set; }

    public int YearOfRelease { get; set; }

    public string Description { get; set; }


    public Guid TypeOfContentId { get; set; }

    public Guid DirectorId { get; set; }
    public Guid ImageId { get; set; }

    public List<Guid> GenreIds { get; set; } = new();
}