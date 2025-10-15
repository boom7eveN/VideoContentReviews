using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;

public static class ImageConfiguration
{
    public static void ConfigureImage(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Image>().HasKey(i => i.Id);
    }
}