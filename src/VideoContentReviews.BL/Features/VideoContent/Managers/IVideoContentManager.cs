using VideoContentReviews.BL.Features.VideoContent.DTOs;

namespace VideoContentReviews.BL.Features.VideoContent.Managers;

public interface IVideoContentManager
{
    Task<VideoContentModel> CreateVideoContentAsync(CreateVideoContentModel model);
    Task DeleteVideoContentAsync(Guid id);
    Task<VideoContentModel> UpdateVideoContentAsync(Guid id, UpdateVideoContentModel model);
}