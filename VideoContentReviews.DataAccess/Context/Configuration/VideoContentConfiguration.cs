using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;

public static class VideoContentConfiguration
{
    public static void ConfigureVideoContent(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VideoContent>().HasKey(vc => vc.Id);

        modelBuilder.Entity<VideoContent>().HasOne(vc => vc.TypeOfContent)
            .WithMany(tc => tc.VideoContents)
            .HasForeignKey(vc => vc.TypeOfContentId);
        
        modelBuilder.Entity<VideoContent>().HasOne(i => i.Image)
            .WithMany(i=>i.VideoContents)
            .HasForeignKey(vc => vc.ImageId);
        
        modelBuilder.Entity<VideoContent>().HasOne(vc => vc.Director)
            .WithMany(d=> d.VideoContents)
            .HasForeignKey(vc => vc.DirectorId);
    }
}