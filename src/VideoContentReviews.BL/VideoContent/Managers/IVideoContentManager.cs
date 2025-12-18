using VideoContentReviews.BL.VideoContent.Entities;

namespace VideoContentReviews.BL.VideoContent.Managers;

public interface IVideoContentManager
{
    Task<VideoContentModel> CreateVideoContentAsync(CreateVideoContentModel model);
}