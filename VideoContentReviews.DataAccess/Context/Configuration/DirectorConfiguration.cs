using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;
public static class DirectorConfiguration
{
    public static void ConfigureDirector(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DirectorEntity>().HasKey(d => d.Id);
        modelBuilder.Entity<DirectorEntity>()
            .HasIndex(d => d.ExternalId)
            .IsUnique();
    }
}