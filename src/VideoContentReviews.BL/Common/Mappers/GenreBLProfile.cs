using AutoMapper;
using VideoContentReviews.BL.Features.Genres.DTOs;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.BL.Common.Mappers;

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