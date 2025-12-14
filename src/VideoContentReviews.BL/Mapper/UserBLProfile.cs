using AutoMapper;
using VideoContentReviews.BL.User.Entities;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.BL.Mapper;

public class UserBLProfile : Profile
{
    public UserBLProfile()
    {
        CreateMap<UserEntity, UserModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.CreationTime))
            .ForMember(dest => dest.ModificationTime, opt => opt.MapFrom(src => src.ModificationTime))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => 
                src.Role.ToString())); 
    }
}