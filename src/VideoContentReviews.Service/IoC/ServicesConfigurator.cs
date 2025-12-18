using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VideoContentReviews.BL.Auth;
using VideoContentReviews.BL.Directors.Managers;
using VideoContentReviews.BL.Genres.Managers;
using VideoContentReviews.BL.Images.Managers;
using VideoContentReviews.BL.TypesOfContent.Managers;
using VideoContentReviews.BL.VideoContent.Managers;
using VideoContentReviews.BL.VideoContent.Providers;
using VideoContentReviews.DataAccess.Context;
using VideoContentReviews.DataAccess.Entities;
using VideoContentReviews.DataAccess.Repositories;
using VideoContentReviews.Service.Settings;

namespace VideoContentReviews.Service.IoC;

public static class ServicesConfigurator
{
    public static void ConfigureServices(IServiceCollection services, VideoContentReviewsSettings settings)
    {
        
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        //videocontent
        services.AddScoped<IVideoContentManager>(provider =>
            new VideoContentManager(
                provider.GetRequiredService<IRepository<VideoContentEntity>>(),
                provider.GetRequiredService<IRepository<TypeOfContentEntity>>(),
                provider.GetRequiredService<IRepository<DirectorEntity>>(),
                provider.GetRequiredService<IRepository<ImageEntity>>(),
                provider.GetRequiredService<IRepository<GenreEntity>>(),
                provider.GetRequiredService<IRepository<VideoContentGenreEntity>>(),
                provider.GetRequiredService<IDbContextFactory<VideoContentReviewsDbContext>>(),
                provider.GetRequiredService<IMapper>()
            ));
        
        services.AddScoped<IVideoContentProvider>(provider =>
            new VideoContentProvider(
                provider.GetRequiredService<IRepository<VideoContentEntity>>(),
                provider.GetRequiredService<IMapper>(),
                provider.GetRequiredService<IDbContextFactory<VideoContentReviewsDbContext>>()
            ));
        //auth
        services.AddScoped<IAuthProvider>(x =>
            new AuthProvider(x.GetRequiredService<SignInManager<UserEntity>>(),
                x.GetRequiredService<UserManager<UserEntity>>(),
                x.GetRequiredService<IHttpClientFactory>(),
                x.GetRequiredService<IMapper>(),
                settings.IdentityServerUri,
                settings.ClientId,
                settings.ClientSecret));
        
        //directors
        services.AddScoped<IDirectorManager>(provider =>
        {
            var repository = provider.GetRequiredService<IRepository<DirectorEntity>>();
            var mapper = provider.GetRequiredService<IMapper>();
            return new DirectorManager(repository, mapper);
        });
        //content
        services.AddScoped<ITypeOfContentManager>(provider =>
        {
            var repository = provider.GetRequiredService<IRepository<TypeOfContentEntity>>();
            var mapper = provider.GetRequiredService<IMapper>();
            return new TypeOfContentManager(repository, mapper);
        });
        //images
        services.AddScoped<IImageManager>(provider =>
        {
            var repository = provider.GetRequiredService<IRepository<ImageEntity>>();
            var mapper = provider.GetRequiredService<IMapper>();
            return new ImageManager(repository, mapper);
        });
        //genres
        services.AddScoped<IGenreManager>(provider =>
        {
            var repository = provider.GetRequiredService<IRepository<GenreEntity>>();
            var mapper = provider.GetRequiredService<IMapper>();
            return new GenreManager(repository, mapper);
        });
    }
}