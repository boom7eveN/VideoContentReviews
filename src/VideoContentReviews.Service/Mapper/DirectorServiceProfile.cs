using AutoMapper;
using VideoContentReviews.BL.Features.Directors.DTOs;
using VideoContentReviews.Service.Controllers.Directors.DTOs.Requests;
using VideoContentReviews.Service.Controllers.Directors.DTOs.Responses;

namespace VideoContentReviews.Service.Mapper;

public class DirectorServiceProfile : Profile
{
    public DirectorServiceProfile()
    {
        CreateMap<CreateDirectorRequest, CreateDirectorModel>()
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Patronymic,
                opt => opt.MapFrom(src => src.Patronymic));

        CreateMap<DirectorModel, DirectorResponse>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Patronymic,
                opt => opt.MapFrom(src => src.Patronymic));
    }
}