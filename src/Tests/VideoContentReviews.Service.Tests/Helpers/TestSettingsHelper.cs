using VideoContentReviews.Service.Settings;

namespace VideoContentReviews.Service.Tests.Helpers;

public static class TestSettingsHelper
{
    public static VideoContentReviewsSettings GetSettings()
    {
        return VideoContentReviewsSettingsReader.Read(ConfigurationHelper.GetConfiguration());
    }
}