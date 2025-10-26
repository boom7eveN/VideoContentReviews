using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;

public static class VideoContentGenreConfiguration
{
    public static void ConfigureVideoContentGenre(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VideoContentGenreEntity>().HasKey(vcg => new { vcg.Id});
        modelBuilder.Entity<VideoContentGenreEntity>().HasIndex(vcg => new { vcg.ExternalId}).IsUnique();
        
        modelBuilder.Entity<VideoContentGenreEntity>()
            .HasIndex(vcg => new { vcg.VideoContentId, vcg.GenreId })
            .IsUnique();
        
        modelBuilder.Entity<VideoContentGenreEntity>().HasOne(vcg => vcg.VideoContentEntity)
            .WithMany(vc => vc.VideoContentsGenres)
            .HasForeignKey(vcg => vcg.VideoContentId);
        
        modelBuilder.Entity<VideoContentGenreEntity>().HasOne(vcg => vcg.GenreEntity)
            .WithMany(g => g.VideoContentsGenres)
            .HasForeignKey(vcg => vcg.GenreId);
    }
}
