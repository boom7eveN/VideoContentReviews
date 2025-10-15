using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Context;
using VideoContentReviews.Service.Settings;

namespace VideoContentReviews.Service.IoC;

public static class DbContextConfigurator
{
    public static void ConfigureService(IServiceCollection services, VideoContentReviewsSettings settings)
    {
        services.AddDbContextFactory<VideoContentReviewsDbContext>(options =>
        {
            options.UseNpgsql(settings.VideoContentReviewsDbConnectionString);
        }, ServiceLifetime.Scoped);

    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<VideoContentReviewsDbContext>>();
        using var context = contextFactory.CreateDbContext();
        context.Database.Migrate();
    }
}