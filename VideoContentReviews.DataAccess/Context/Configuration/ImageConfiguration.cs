using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;

public static class ImageConfiguration
{
    public static void ConfigureImage(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImageEntity>().HasKey(i => i.Id);
        modelBuilder.Entity<ImageEntity>()
            .HasIndex(i => i.ExternalId)
            .IsUnique();
    }
}