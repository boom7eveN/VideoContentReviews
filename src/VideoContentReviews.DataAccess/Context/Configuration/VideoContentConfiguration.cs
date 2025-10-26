using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;

public static class VideoContentConfiguration
{
    public static void ConfigureVideoContent(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VideoContentEntity>().HasKey(vc => vc.Id);
        modelBuilder.Entity<VideoContentEntity>().HasIndex(vc => vc.ExternalId).IsUnique();

        modelBuilder.Entity<VideoContentEntity>().HasOne(vc => vc.TypeOfContentEntity)
            .WithMany(tc => tc.VideoContents)
            .HasForeignKey(vc => vc.TypeOfContentId);
        
        modelBuilder.Entity<VideoContentEntity>().HasOne(i => i.ImageEntity)
            .WithMany(i=>i.VideoContents)
            .HasForeignKey(vc => vc.ImageId);
        
        modelBuilder.Entity<VideoContentEntity>().HasOne(vc => vc.DirectorEntity)
            .WithMany(d=> d.VideoContents)
            .HasForeignKey(vc => vc.DirectorId);
    }
}