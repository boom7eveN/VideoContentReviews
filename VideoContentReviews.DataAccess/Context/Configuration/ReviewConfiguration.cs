using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;

public static class ReviewConfiguration
{
    public static void ConfigureReviews(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReviewEntity>().HasKey(r => r.Id);
        
        modelBuilder.Entity<ReviewEntity>()
            .HasIndex(r => r.ExternalId)
            .IsUnique();
        
        
        modelBuilder.Entity<ReviewEntity>().HasOne(r => r.UserEntity)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId);
        
        modelBuilder.Entity<ReviewEntity>().HasOne(r => r.VideoContentEntity)
            .WithMany(vc => vc.Reviews)
            .HasForeignKey(r => r.VideoContentId);
        
    }
}