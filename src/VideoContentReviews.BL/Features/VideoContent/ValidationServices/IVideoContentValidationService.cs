using VideoContentReviews.BL.Features.VideoContent.DTOs;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.BL.Features.VideoContent.ValidationServices;

public interface IVideoContentValidationService
{
    Task<TypeOfContentEntity> ValidateAndGetTypeOfContentAsync(Guid externalId);
    Task<DirectorEntity> ValidateAndGetDirectorAsync(Guid externalId);
    Task<ImageEntity> ValidateAndGetImageAsync(Guid externalId);
    Task<List<GenreEntity>> ValidateAndGetGenresAsync(List<Guid> genreExternalIds);
}