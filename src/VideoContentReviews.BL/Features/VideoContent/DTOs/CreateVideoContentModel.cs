namespace VideoContentReviews.BL.Features.VideoContent.DTOs;

public class CreateVideoContentModel
{
    public string Name { get; set; }
    public int YearOfRelease { get; set; }
    public string Description { get; set; }

    public Guid TypeOfContentExternalId { get; set; }
    public Guid DirectorExternalId { get; set; }
    public Guid ImageExternalId { get; set; }

    public List<Guid> GenreExternalIds { get; set; } = new();
}