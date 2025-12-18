using VideoContentReviews.BL.VideoContent.Entities;

namespace VideoContentReviews.BL.VideoContent.Providers;

public interface IVideoContentProvider
{
    Task<List<VideoContentModel>> GetAllAsync();
}