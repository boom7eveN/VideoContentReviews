using VideoContentReviews.BL.Features.VideoContent.DTOs;

namespace VideoContentReviews.BL.Features.VideoContent.Providers;

public interface IVideoContentProvider
{
    Task<List<VideoContentModel>> GetAllAsync();
    Task<VideoContentModel?> GetByIdAsync(Guid externalId);
}