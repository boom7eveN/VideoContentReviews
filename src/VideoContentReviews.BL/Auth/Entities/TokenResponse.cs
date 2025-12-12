namespace VideoContentReviews.BL.Auth.Entities;

public class TokenResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}