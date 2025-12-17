using VideoContentReviews.BL.Directors.Entities;

namespace VideoContentReviews.BL.Directors.Managers;

public interface IDirectorManager
{
    Task<DirectorModel> CreateDirectorAsync(CreateDirectorModel model);

}