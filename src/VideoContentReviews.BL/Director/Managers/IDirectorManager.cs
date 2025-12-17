using VideoContentReviews.BL.Director.Entities;

namespace VideoContentReviews.BL.Director.Managers;

public interface IDirectorManager
{
    Task<DirectorModel> CreateDirectorAsync(CreateDirectorModel model);

}