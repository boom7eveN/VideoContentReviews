using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Context.Configuration;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context;

public class VideoContentReviewsDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<VideoContent> VideoContents { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<TypeOfContent> TypeOfContents { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<VideoContentGenre> VideoContentsGenres { get; set; }
    public DbSet<Favourite> Favourites { get; set; }

    public VideoContentReviewsDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureDirector();
        modelBuilder.ConfigureFavorite();
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