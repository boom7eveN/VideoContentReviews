using VideoContentReviews.BL.Common.Exceptions;
using VideoContentReviews.BL.Features.VideoContent.DTOs;
using VideoContentReviews.DataAccess.Entities;
using VideoContentReviews.DataAccess.Repositories;
using VideoContentReviews.DataAccess.Repositories.VideoContentRepository;

namespace VideoContentReviews.BL.Features.VideoContent.ValidationServices;

public class VideoContentValidationService(
    IVideoContentRepository videoContentRepository,
    IRepository<TypeOfContentEntity> typeOfContentRepository,
    IRepository<DirectorEntity> directorRepository,
    IRepository<ImageEntity> imageRepository,
    IRepository<GenreEntity> genreRepository)
    : IVideoContentValidationService
{
    public async Task<TypeOfContentEntity> ValidateAndGetTypeOfContentAsync(Guid externalId)
    {
        var typeOfContent = await typeOfContentRepository.GetByIdAsync(externalId);
        if (typeOfContent == null)
            throw new BusinessLogicException(BLResultCode.TypeOfContentNotFound);
        return typeOfContent;
    }

    public async Task<DirectorEntity> ValidateAndGetDirectorAsync(Guid externalId)
    {
        var director = await directorRepository.GetByIdAsync(externalId);
        if (director == null)
            throw new BusinessLogicException(BLResultCode.DirectorNotFound);
        return director;
    }

    public async Task<ImageEntity> ValidateAndGetImageAsync(Guid externalId)
    {
        var image = await imageRepository.GetByIdAsync(externalId);
        if (image == null)
            throw new BusinessLogicException(BLResultCode.ImageNotFound);
        return image;
    }

    public async Task<List<GenreEntity>> ValidateAndGetGenresAsync(List<Guid> genreExternalIds)
    {
        var genres = new List<GenreEntity>();
        foreach (var genreExternalId in genreExternalIds)
        {
            var genre = await genreRepository.GetByIdAsync(genreExternalId);
            if (genre == null)
                throw new BusinessLogicException(BLResultCode.GenreNotFound,
                    $"Genre with external ID {genreExternalId} not found");
            genres.Add(genre);
        }

        return genres;
    }

    public async Task ValidateNoDuplicateAsync(string name, int yearOfRelease)
    {
        var sameVideoContent = await videoContentRepository.GetAllAsync(x =>
            x.Name == name && x.YearOfRelease == yearOfRelease);
        if (sameVideoContent.Any())
            throw new BusinessLogicException(BLResultCode.VideoContentAlreadyExists);
    }
}