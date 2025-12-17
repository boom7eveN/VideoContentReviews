using AutoMapper;
using VideoContentReviews.BL.Directors.Entities;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.BL.Mappers;

public class DirectorBLProfile : Profile
{
    public DirectorBLProfile()
    {
        CreateMap<DirectorEntity, DirectorModel>()
            .ForMember(dest => dest.ExternalId,
                opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Patronymic,
                opt => opt.MapFrom(src => src.Patronymic));
        
        CreateMap<CreateDirectorModel, DirectorEntity>()
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Patronymic,
                opt => opt.MapFrom(src => src.Patronymic));
    }
}