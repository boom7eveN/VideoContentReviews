using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;

public static class ReviewsConfiguration
{
    public static void ConfigureReviews(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>().HasKey(r => r.Id);
        
        modelBuilder.Entity<Review>().HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId);
        
        modelBuilder.Entity<Review>().HasOne(r => r.VideoContent)
            .WithMany(vc => vc.Reviews)
            .HasForeignKey(r => r.VideoContentId);
    }
}