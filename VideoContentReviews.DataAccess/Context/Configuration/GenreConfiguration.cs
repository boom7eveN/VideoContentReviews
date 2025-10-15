using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;

public static class GenreConfiguration
{
    public static void ConfigureGenre(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasKey(g => g.Id);
        
    }
}