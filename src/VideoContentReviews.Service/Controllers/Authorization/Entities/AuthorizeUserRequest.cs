namespace VideoContentReviews.Service.Controllers.Authorization.Entities;

public class AuthorizeUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}