namespace VideoContentReviews.BL.Features.Auth.DTOs;

public class TokensResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}