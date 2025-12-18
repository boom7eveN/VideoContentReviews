using VideoContentReviews.BL.Genres.Entities;

namespace VideoContentReviews.BL.Genres.Managers;

public interface IGenreManager
{
    Task<GenreModel> CreateGenreAsync(CreateGenreModel model);
}