using VideoContentReviews.BL.Auth.Entities;
using VideoContentReviews.BL.User.Entities;

namespace VideoContentReviews.BL.Auth;

public interface IAuthProvider
{
    Task<TokenResponse> AuthorizeUserAsync(AuthorizeUserModel model);
    Task<UserModel> RegisterUserAsync(RegisterUserModel model);
}