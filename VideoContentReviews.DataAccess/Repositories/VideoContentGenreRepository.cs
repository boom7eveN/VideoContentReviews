using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Context;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Repositories;

public class VideoContentGenreRepository : IVideoContentGenreRepository
{
    private readonly IDbContextFactory<VideoContentReviewsDbContext> _contextFactory;

    public VideoContentGenreRepository(IDbContextFactory<VideoContentReviewsDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public IQueryable<VideoContentGenre> GetAll()
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Set<VideoContentGenre>();
    }

    public VideoContentGenre? GetById(Guid videoContentId, Guid genreId)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Set<VideoContentGenre>()
            .FirstOrDefault(vcg => vcg.VideoContentId == videoContentId && vcg.GenreId == genreId);
    }

    public VideoContentGenre Save(VideoContentGenre entity)
    {
        using var context = _contextFactory.CreateDbContext();
        bool exists = context.Set<VideoContentGenre>()
            .Any(vcg => vcg.VideoContentId == entity.VideoContentId &&
                        vcg.GenreId == entity.GenreId);

        if (exists)
        {
            return context.Set<VideoContentGenre>()
                .First(vcg => vcg.VideoContentId == entity.VideoContentId &&
                              vcg.GenreId == entity.GenreId);
        }

        var result = context.Set<VideoContentGenre>().Add(entity);
        context.SaveChanges();
        return result.Entity;
    }

    public void Delete(VideoContentGenre entity)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<VideoContentGenre>().Attach(entity);
        context.Entry(entity).State = EntityState.Deleted;
        context.SaveChanges();
    }
}