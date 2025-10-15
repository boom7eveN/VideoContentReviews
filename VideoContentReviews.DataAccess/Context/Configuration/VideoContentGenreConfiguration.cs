using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;

public static class VideoContentGenreConfiguration
{
    public static void ConfigureVideoContentGenre(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VideoContentGenre>().HasKey(vcg => new { vcg.VideoContentId, vcg.GenreId });
        
        modelBuilder.Entity<VideoContentGenre>().HasOne(vcg => vcg.VideoContent)
            .WithMany(vc => vc.VideoContentsGenres)
            .HasForeignKey(vcg => vcg.VideoContentId);
        
        modelBuilder.Entity<VideoContentGenre>().HasOne(vcg => vcg.Genre)
            .WithMany(g => g.VideoContentsGenres)
            .HasForeignKey(vcg => vcg.GenreId);
    }
}
