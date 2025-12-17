namespace VideoContentReviews.BL.Directors.Entities;

public class DirectorModel
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronymic { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
}