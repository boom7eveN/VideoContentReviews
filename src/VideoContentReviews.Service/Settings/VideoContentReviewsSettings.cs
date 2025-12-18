namespace VideoContentReviews.Service.Settings;

public class VideoContentReviewsSettings
{
    public string VideoContentReviewsDbConnectionString { get; set; }
    public string IdentityServerUri { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string MasterAdminEmail { get; set; }
    public string MasterAdminPassword { get; set; }
    public string MasterUserName { get; set; }
}