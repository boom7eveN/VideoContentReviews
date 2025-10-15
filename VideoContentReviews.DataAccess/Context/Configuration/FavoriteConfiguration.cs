using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;

public static class FavoriteConfiguration
{
    public static void ConfigureFavorite(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Favourite>().HasKey(f => f.Id);
        
        modelBuilder.Entity<Favourite>().HasOne(f => f.User)
            .WithMany(u => u.Favourites)
            .HasForeignKey(f => f.UserId);

        modelBuilder.Entity<Favourite>().HasOne(f => f.VideoContent)
            .WithMany(vc => vc.Favourites)
            .HasForeignKey(f => f.VideoContentId);
    }
}
