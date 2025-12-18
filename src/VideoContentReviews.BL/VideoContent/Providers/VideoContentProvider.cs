using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VideoContentReviews.BL.VideoContent.Entities;
using VideoContentReviews.DataAccess.Context;
using VideoContentReviews.DataAccess.Entities;
using VideoContentReviews.DataAccess.Repositories;

namespace VideoContentReviews.BL.VideoContent.Providers;

public class VideoContentProvider(
    IRepository<VideoContentEntity> videoContentRepository,
    IMapper mapper,
    IDbContextFactory<VideoContentReviewsDbContext> contextFactory) : IVideoContentProvider
{
    public async Task<List<VideoContentModel>> GetAllAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var videoContents = await context.VideoContents
            .Include(vc => vc.TypeOfContentEntity)
            .Include(vc => vc.DirectorEntity)
            .Include(vc => vc.ImageEntity)
            .Include(vc => vc.VideoContentsGenres)
            .ThenInclude(vcg => vcg.GenreEntity)
            .ToListAsync();

        return mapper.Map<List<VideoContentModel>>(videoContents);
    }

    public async Task<VideoContentModel?> GetByIdAsync(Guid externalId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        var videoContent = await context.VideoContents
            .Include(vc => vc.TypeOfContentEntity)
            .Include(vc => vc.DirectorEntity)
            .Include(vc => vc.ImageEntity)
            .Include(vc => vc.VideoContentsGenres)
            .ThenInclude(vcg => vcg.GenreEntity)
            .FirstOrDefaultAsync(vc => vc.ExternalId == externalId);

        if (videoContent == null)
            return null;

        return mapper.Map<VideoContentModel>(videoContent);
    }
}