using AutoMapper;
using VideoContentReviews.BL.Genres.Entities;
using VideoContentReviews.Service.Controllers.Genres.DTOs;
using VideoContentReviews.Service.Controllers.Genres.DTOs.Responses;

namespace VideoContentReviews.Service.Mapper;

public class GenreServiceProfile : Profile
{
    public GenreServiceProfile()
    {
        CreateMap<CreateGenreRequest, CreateGenreModel>()
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Title));

        CreateMap<GenreModel, GenreResponse>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Title));
    }
}