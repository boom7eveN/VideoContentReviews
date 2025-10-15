using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Repositories;

public interface IVideoContentGenreRepository
{
    IQueryable<VideoContentGenre> GetAll();
    VideoContentGenre? GetById(Guid videoContentId, Guid genreId);
    VideoContentGenre Save(VideoContentGenre entity);
    void Delete(VideoContentGenre entity);
}