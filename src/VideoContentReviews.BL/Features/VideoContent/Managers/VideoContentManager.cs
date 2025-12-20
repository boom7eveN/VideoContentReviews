using AutoMapper;
using Duende.IdentityServer.Extensions;
using VideoContentReviews.BL.Common.Exceptions;
using VideoContentReviews.BL.Features.VideoContent.DTOs;
using VideoContentReviews.BL.Features.VideoContent.ValidationServices;
using VideoContentReviews.BL.Features.VideoContent.Validators;
using VideoContentReviews.DataAccess.Entities;
using VideoContentReviews.DataAccess.Repositories;
using VideoContentReviews.DataAccess.Repositories.VideoContentRepository;

namespace VideoContentReviews.BL.Features.VideoContent.Managers;

public class VideoContentManager(
    IVideoContentRepository videoContentRepository,
    IVideoContentValidationService videoContentValidationService,
    IRepository<VideoContentGenreEntity> videoContentGenreRepository,
    IMapper mapper)
    : IVideoContentManager
{
    public async Task<VideoContentModel> CreateVideoContentAsync(CreateVideoContentModel model)
    {
        var validator = new CreateVideoContentModelValidator();
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            throw new BusinessLogicException(BLResultCode.ValidationError,
                string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var sameVideoContent =
            await videoContentRepository.GetAllAsync(x =>
                x.Name == model.Name && x.YearOfRelease == model.YearOfRelease);

        if (sameVideoContent.Any())
        {
            throw new BusinessLogicException(BLResultCode.VideoContentAlreadyExists);
        }

        var typeOfContent =
            await videoContentValidationService.ValidateAndGetTypeOfContentAsync(model.TypeOfContentExternalId);
        var director = await videoContentValidationService.ValidateAndGetDirectorAsync(model.DirectorExternalId);
        var image = await videoContentValidationService.ValidateAndGetImageAsync(model.ImageExternalId);
        var genres = await videoContentValidationService.ValidateAndGetGenresAsync(model.GenreExternalIds);

        var entity = mapper.Map<VideoContentEntity>(model);
        entity.TypeOfContentId = typeOfContent.Id;
        entity.DirectorId = director.Id;
        entity.ImageId = image.Id;

        entity = await videoContentRepository.SaveAsync(entity);

        foreach (var genre in genres)
        {
            var videoContentGenre = new VideoContentGenreEntity
            {
                VideoContentId = entity.Id,
                GenreId = genre.Id,
                AddedTime = DateTime.UtcNow,
            };

            await videoContentGenreRepository.SaveAsync(videoContentGenre);
        }

        var videoContentWithRelations = await videoContentRepository.GetByIdWithRelationsAsync(entity.Id);

        if (videoContentWithRelations == null)
        {
            throw new BusinessLogicException(BLResultCode.VideoContentNotFound);
        }

        var videoContentModel = mapper.Map<VideoContentModel>(videoContentWithRelations);

        return videoContentModel;
    }

    public async Task DeleteVideoContentAsync(Guid id)
    {
        var videoEntity = await videoContentRepository.GetByIdAsync(id);
        if (videoEntity == null)
            throw new BusinessLogicException(BLResultCode.VideoContentNotFound);
        await videoContentRepository.DeleteAsync(videoEntity);
    }

    public async Task<VideoContentModel> UpdateVideoContentAsync(Guid id, UpdateVideoContentModel model)
    {
        var validator = new UpdateVideoContentModelValidator();
        var result = await validator.ValidateAsync(model);
        if (!result.IsValid)
        {
            throw new BusinessLogicException(BLResultCode.ValidationError,
                string.Join(Environment.NewLine, result.Errors.Select(e => e.ErrorMessage)));
        }


        var entity = await videoContentRepository.GetByIdAsync(id);
        if (entity == null)
            throw new BusinessLogicException(BLResultCode.VideoContentNotFound);

        if (!model.Description.IsNullOrEmpty())
            entity.Description = model.Description;

        if (!model.Name.IsNullOrEmpty())
            entity.Name = model.Name;

        if (model.YearOfRelease.HasValue)
            entity.YearOfRelease = model.YearOfRelease.Value;

        entity.ModificationTime = DateTime.UtcNow;


        entity = await videoContentRepository.SaveAsync(entity);

        var videoContentWithRelations = await videoContentRepository.GetByIdWithRelationsAsync(entity.Id);

        if (videoContentWithRelations == null)
        {
            throw new BusinessLogicException(BLResultCode.VideoContentNotFound);
        }

        return mapper.Map<VideoContentModel>(videoContentWithRelations);
    }
}