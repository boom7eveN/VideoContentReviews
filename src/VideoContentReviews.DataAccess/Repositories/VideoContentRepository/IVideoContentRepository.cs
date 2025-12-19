using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Repositories.VideoContentRepository;

public interface IVideoContentRepository : IRepository<VideoContentEntity>
{
    Task<List<VideoContentEntity>> GetAllWithRelationsAsync();
    Task<VideoContentEntity?> GetByIdWithRelationsAsync(Guid externalId);
    Task<VideoContentEntity?> GetByIdWithRelationsAsync(int id);
}