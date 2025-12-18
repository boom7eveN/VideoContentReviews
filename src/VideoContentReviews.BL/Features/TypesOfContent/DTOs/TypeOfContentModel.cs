namespace VideoContentReviews.BL.Features.TypesOfContent.DTOs;

public class TypeOfContentModel
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
    public string Title { get; set; }
}