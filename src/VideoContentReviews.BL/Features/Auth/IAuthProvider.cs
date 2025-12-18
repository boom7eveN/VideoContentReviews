using VideoContentReviews.BL.Features.Auth.Entities;
using VideoContentReviews.BL.Features.Users.DTOs;

namespace VideoContentReviews.BL.Features.Auth;

public interface IAuthProvider
{
    Task<TokensResponse> AuthorizeUserAsync(AuthorizeUserModel model);
    Task<UserModel> RegisterUserAsync(RegisterUserModel model);
    Task<TokensResponse> RefreshTokenAsync(string refreshToken);
}