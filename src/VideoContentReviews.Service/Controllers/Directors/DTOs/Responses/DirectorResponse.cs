namespace VideoContentReviews.Service.Controllers.Directors.DTOs.Responses;

public class DirectorResponse
{
    public Guid ExternalId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronymic { get; set; }
}