using AutoMapper;
using Microsoft.AspNetCore.Identity;
using VideoContentReviews.BL.Auth;
using VideoContentReviews.BL.Directors.Managers;
using VideoContentReviews.BL.Images.Managers;
using VideoContentReviews.BL.TypesOfContent.Managers;
using VideoContentReviews.DataAccess.Entities;
using VideoContentReviews.DataAccess.Repositories;
using VideoContentReviews.Service.Settings;

namespace VideoContentReviews.Service.IoC;

public static class ServicesConfigurator
{
    public static void ConfigureServices(IServiceCollection services, VideoContentReviewsSettings settings)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
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
    }
}