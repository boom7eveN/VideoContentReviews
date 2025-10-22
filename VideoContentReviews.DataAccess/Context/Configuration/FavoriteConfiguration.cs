using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;

public static class FavoriteConfiguration
{
    public static void ConfigureFavorite(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FavouriteEntity>().HasKey(f => f.Id);
        
        modelBuilder.Entity<FavouriteEntity>()
            .HasIndex(f => f.ExternalId)
            .IsUnique();
        
        modelBuilder.Entity<FavouriteEntity>().HasOne(f => f.UserEntity)
            .WithMany(u => u.Favourites)
            .HasForeignKey(f => f.UserId);

        modelBuilder.Entity<FavouriteEntity>().HasOne(f => f.VideoContentEntity)
            .WithMany(vc => vc.Favourites)
            .HasForeignKey(f => f.VideoContentId);
    }
}
