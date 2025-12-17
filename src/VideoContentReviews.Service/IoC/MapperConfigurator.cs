using VideoContentReviews.BL.Mappers;
using VideoContentReviews.Service.Mapper;

namespace VideoContentReviews.Service.IoC;

public abstract class MapperConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            // users
            config.AddProfile<UsersServiceProfile>();
            config.AddProfile<UserBLProfile>();
            // auth
            config.AddProfile<AuthBLProfile>();
            //directors
            config.AddProfile<DirectorServiceProfile>();
            config.AddProfile<DirectorBLProfile>();
            //content
            config.AddProfile<TypeOfContentBLProfile>();
            config.AddProfile<TypeOfContentServiceProfile>();
            //image
            config.AddProfile<ImageServiceProfile>();
            config.AddProfile<ImageBLProfile>();
            //genre
            //image
            config.AddProfile<GenreServiceProfile>();
            config.AddProfile<GenreBLProfile>();
        });
    }
}