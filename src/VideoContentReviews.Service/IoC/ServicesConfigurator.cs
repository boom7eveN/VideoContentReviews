using AutoMapper;
using Microsoft.AspNetCore.Identity;
using VideoContentReviews.BL.Features.Auth;
using VideoContentReviews.BL.Features.Directors.Managers;
using VideoContentReviews.BL.Features.Genres.Managers;
using VideoContentReviews.BL.Features.Images.Managers;
using VideoContentReviews.BL.Features.TypesOfContent.Managers;
using VideoContentReviews.BL.Features.VideoContent.Managers;
using VideoContentReviews.BL.Features.VideoContent.Providers;
using VideoContentReviews.DataAccess.Entities;
using VideoContentReviews.DataAccess.Repositories;
using VideoContentReviews.DataAccess.Repositories.VideoContentRepository;
using VideoContentReviews.Service.Settings;

namespace VideoContentReviews.Service.IoC;

public static class ServicesConfigurator
{
    public static void ConfigureServices(IServiceCollection services, VideoContentReviewsSettings settings)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IVideoContentRepository, VideoContentRepository>();
        
        //videocontent
        services.AddScoped<IVideoContentManager>(provider =>
            new VideoContentManager(
                provider.GetRequiredService<IVideoContentRepository>(),
                provider.GetRequiredService<IRepository<TypeOfContentEntity>>(),
                provider.GetRequiredService<IRepository<DirectorEntity>>(),
                provider.GetRequiredService<IRepository<ImageEntity>>(),
                provider.GetRequiredService<IRepository<GenreEntity>>(),
                provider.GetRequiredService<IRepository<VideoContentGenreEntity>>(),
                provider.GetRequiredService<IMapper>()
            ));

        services.AddScoped<IVideoContentProvider>(provider =>
            new VideoContentProvider(
                provider.GetRequiredService<IVideoContentRepository>(),
                provider.GetRequiredService<IMapper>()
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