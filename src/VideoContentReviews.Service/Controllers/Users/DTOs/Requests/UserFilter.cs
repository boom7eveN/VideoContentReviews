namespace VideoContentReviews.Service.Controllers.Users.DTOs.Requests;

public class UserFilter
{
    public string? UserNamePart { get; set; }
    public string? EmailPart { get; set; }
    public string? Role { get; set; }
}