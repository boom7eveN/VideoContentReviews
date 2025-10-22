using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Context.Configuration;

public static class UserConfiguration
{
    public static void ConfigureUser(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasKey(u => u.Id);
        modelBuilder.Entity<UserEntity>().HasIndex(u => u.ExternalId).IsUnique();
        modelBuilder.Entity<UserEntity>().HasOne(u => u.UserRoleEntity)
            .WithMany(ur => ur.Users)
            .HasForeignKey(u => u.UserRoleId);
    }
}