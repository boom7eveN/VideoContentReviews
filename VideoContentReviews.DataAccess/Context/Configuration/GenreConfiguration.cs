using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;

public static class GenreConfiguration
{
    public static void ConfigureGenre(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GenreEntity>().HasKey(g => g.Id);
        modelBuilder.Entity<GenreEntity>()
            .HasIndex(g => g.ExternalId)
            .IsUnique();
    }
}