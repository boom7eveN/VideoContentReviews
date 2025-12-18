using VideoContentReviews.BL.VideoContent.Entities;

namespace VideoContentReviews.BL.VideoContent.Managers;

public interface IVideoContentManager
{
    Task<VideoContentModel> CreateVideoContentAsync(CreateVideoContentModel model);
    Task DeleteVideoContentAsync(Guid id);
    Task<VideoContentModel> UpdateVideoContentAsync(Guid id, UpdateVideoContentModel model);
}