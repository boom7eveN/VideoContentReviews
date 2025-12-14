using VideoContentReviews.BL.Auth.Entities;
using VideoContentReviews.BL.User.Entities;

namespace VideoContentReviews.BL.Auth;

public interface IAuthProvider
{
    Task<TokensResponse> AuthorizeUserAsync(AuthorizeUserModel model);
    Task<UserModel> RegisterUserAsync(RegisterUserModel model);
    Task<TokensResponse> RefreshTokenAsync(string refreshToken);
}