using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;

public static class UserRoleConfiguration
{
    public static void ConfigureUserRole(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>().HasKey(ur => ur.Id);
    }
}