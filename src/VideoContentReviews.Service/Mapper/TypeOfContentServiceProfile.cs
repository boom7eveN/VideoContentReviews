using AutoMapper;
using VideoContentReviews.BL.Features.TypesOfContent.DTOs;
using VideoContentReviews.Service.Controllers.TypeOfContent.DTOs.Requests;
using VideoContentReviews.Service.Controllers.TypeOfContent.DTOs.Responses;

namespace VideoContentReviews.Service.Mapper;

public class TypeOfContentServiceProfile : Profile
{
    public TypeOfContentServiceProfile()
    {
        CreateMap<CreateTypeOfContentRequest, CreateTypeOfContentModel>()
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Title));

        CreateMap<TypeOfContentModel, TypeOfContentResponse>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Title));
    }
}