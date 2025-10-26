using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;

public static class TypeOfContentConfiguration
{
    public static void ConfigureTypeOfContent(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TypeOfContentEntity>().HasKey(tc => tc.Id);
        modelBuilder.Entity<TypeOfContentEntity>()
            .HasIndex(t => t.ExternalId)
            .IsUnique();
    }
}