using AutoMapper;
using VideoContentReviews.BL.Genres.Entities;
using VideoContentReviews.BL.TypesOfContent.Entities;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.BL.Mappers;

public class GenreBLProfile : Profile
{
    public GenreBLProfile()
    {
        CreateMap<GenreEntity, GenreModel>()
            .ForMember(dest => dest.ExternalId,
                opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Title));

        CreateMap<CreateGenreModel, GenreEntity>()
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Title));
    }
}