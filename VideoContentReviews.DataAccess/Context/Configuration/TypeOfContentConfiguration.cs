using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;

public static class TypeOfContentConfiguration
{
    public static void ConfigureTypeOfContent(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TypeOfContent>().HasKey(tc => tc.Id);
        
    }
}