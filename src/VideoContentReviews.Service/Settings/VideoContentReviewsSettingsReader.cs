namespace VideoContentReviews.Service.Settings
{
    public static class VideoContentReviewsSettingsReader
    {
        public static VideoContentReviewsSettings Read(IConfiguration configuration)
        {
            return new VideoContentReviewsSettings()
            {
                VideoContentReviewsDbConnectionString = configuration.GetConnectionString("VideoContentReviewsDbContext"),
                IdentityServerUri = configuration.GetValue<string>("IdentityServerSettings:Uri"),
                ClientId = configuration.GetValue<string>("IdentityServerSettings:ClientId"),
                ClientSecret = configuration.GetValue<string>("IdentityServerSettings:ClientSecret"),
                MasterAdminEmail = configuration.GetValue<string>("IdentityServerSettings:MasterAdminEmail"),
                MasterAdminPassword = configuration.GetValue<string>("IdentityServerSettings:MasterAdminPassword")
            };
        }
    }
}