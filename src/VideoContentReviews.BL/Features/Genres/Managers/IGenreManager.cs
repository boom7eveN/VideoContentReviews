using VideoContentReviews.BL.Features.Genres.Entities;

namespace VideoContentReviews.BL.Features.Genres.Managers;

public interface IGenreManager
{
    Task<GenreModel> CreateGenreAsync(CreateGenreModel model);
}