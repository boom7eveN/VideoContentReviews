using AutoMapper;
using VideoContentReviews.BL.TypeOfContent.Entities;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.BL.Mapper;

public class TypeOfContentBLProfile : Profile
{
    public TypeOfContentBLProfile()
    {
        CreateMap<TypeOfContentEntity, TypeOfContentModel>()
            .ForMember(dest => dest.ExternalId,
                opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Title));

        CreateMap<CreateTypeOfContentModel, TypeOfContentEntity>()
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Title));
    }
}