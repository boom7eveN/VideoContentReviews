using AutoMapper;
using VideoContentReviews.BL.Common.Exceptions;
using VideoContentReviews.BL.Features.VideoContent.DTOs;
using VideoContentReviews.DataAccess.Repositories;
using VideoContentReviews.DataAccess.Repositories.VideoContentRepository;

namespace VideoContentReviews.BL.Features.VideoContent.Providers;

public class VideoContentProvider(
    IVideoContentRepository videoContentRepository,
    IMapper mapper) : IVideoContentProvider
{
    public async Task<List<VideoContentModel>> GetAllAsync()
    {
        var videoContents = await videoContentRepository.GetAllWithRelationsAsync();
        return mapper.Map<List<VideoContentModel>>(videoContents);
    }

    public async Task<VideoContentModel?> GetByIdAsync(Guid externalId)
    {
        var videoContent = await videoContentRepository.GetByIdWithRelationsAsync(externalId);

        if (videoContent == null)
            throw new BusinessLogicException(BLResultCode.VideoContentNotFound);

        return mapper.Map<VideoContentModel>(videoContent);
    }
}