namespace VideoContentReviews.Service.Controllers.Directors.DTOs.Requests;

public class CreateDirectorRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronymic { get; set; }
}