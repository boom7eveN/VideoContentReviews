using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Context.Configuration;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context;

public class VideoContentReviewsDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserRoleEntity> UserRoles { get; set; }
    public DbSet<VideoContentEntity> VideoContents { get; set; }
    public DbSet<DirectorEntity> Directors { get; set; }
    public DbSet<TypeOfContentEntity> TypeOfContents { get; set; }
    public DbSet<GenreEntity> Genres { get; set; }
    public DbSet<ImageEntity> Images { get; set; }
    public DbSet<ReviewEntity> Reviews { get; set; }
    public DbSet<VideoContentGenreEntity> VideoContentsGenres { get; set; }
    public DbSet<FavouriteEntity> Favourites { get; set; }

    public VideoContentReviewsDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureDirector();
        modelBuilder.ConfigureFavourite();
        modelBuilder.ConfigureGenre();
        modelBuilder.ConfigureImage();
        modelBuilder.ConfigureReviews();
        modelBuilder.ConfigureTypeOfContent();
        modelBuilder.ConfigureUser();
        modelBuilder.ConfigureUserRole();
        modelBuilder.ConfigureVideoContent();
        modelBuilder.ConfigureVideoContentGenre();
    }
}