using VideoContentReviews.BL.Features.Directors.Entities;

namespace VideoContentReviews.BL.Features.Directors.Managers;

public interface IDirectorManager
{
    Task<DirectorModel> CreateDirectorAsync(CreateDirectorModel model);
}