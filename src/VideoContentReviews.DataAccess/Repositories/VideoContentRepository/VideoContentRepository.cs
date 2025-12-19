using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Context;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Repositories.VideoContentRepository;

public class VideoContentRepository(IDbContextFactory<VideoContentReviewsDbContext> contextFactory)
    : Repository<VideoContentEntity>(contextFactory), IVideoContentRepository
{
    private readonly IDbContextFactory<VideoContentReviewsDbContext> _contextFactory = contextFactory;

    public async Task<List<VideoContentEntity>> GetAllWithRelationsAsync()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.VideoContents
            .Include(vc => vc.TypeOfContentEntity)
            .Include(vc => vc.DirectorEntity)
            .Include(vc => vc.ImageEntity)
            .Include(vc => vc.VideoContentsGenres)
            .ThenInclude(vcg => vcg.GenreEntity)
            .ToListAsync();
    }

    public async Task<VideoContentEntity?> GetByIdWithRelationsAsync(Guid externalId)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.VideoContents
            .Include(vc => vc.TypeOfContentEntity)
            .Include(vc => vc.DirectorEntity)
            .Include(vc => vc.ImageEntity)
            .Include(vc => vc.VideoContentsGenres)
            .ThenInclude(vcg => vcg.GenreEntity)
            .FirstOrDefaultAsync(vc => vc.ExternalId == externalId);
    }

    public async Task<VideoContentEntity?> GetByIdWithRelationsAsync(int id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.VideoContents
            .Include(vc => vc.TypeOfContentEntity)
            .Include(vc => vc.DirectorEntity)
            .Include(vc => vc.ImageEntity)
            .Include(vc => vc.VideoContentsGenres)
            .ThenInclude(vcg => vcg.GenreEntity)
            .FirstOrDefaultAsync(vc => vc.Id == id);
    }
}