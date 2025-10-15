namespace VideoContentReviews.Service.Settings
{
    public static class VideoContentReviewsSettingsReader
    {
        public static VideoContentReviewsSettings Read(IConfiguration configuration)
        {
            return new VideoContentReviewsSettings()
            {
                VideoContentReviewsDbConnectionString =
                    configuration.GetConnectionString("VideoContentReviewsDbContext")
            };
        }
    }
}