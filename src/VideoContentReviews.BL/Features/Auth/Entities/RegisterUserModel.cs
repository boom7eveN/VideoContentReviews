using VideoContentReviews.DataAccess.Entities.Primitives;

namespace VideoContentReviews.BL.Features.Auth.Entities;

public class RegisterUserModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
}