using AutoMapper;
using VideoContentReviews.BL.Features.Auth.Entities;
using VideoContentReviews.BL.Features.Users.DTOs;
using VideoContentReviews.Service.Controllers.Authorization.DTOs;
using VideoContentReviews.Service.Controllers.Users.DTOs.Responses;

namespace VideoContentReviews.Service.Mapper;

public class UsersServiceProfile : Profile
{
    public UsersServiceProfile()
    {
        CreateMap<RegisterUserRequest, RegisterUserModel>()
            .ForMember(dest => dest.Role, opt => opt.Ignore());
        CreateMap<AuthorizeUserRequest, AuthorizeUserModel>();
        CreateMap<UserModel, UserResponse>()
            .ForMember(dest => dest.ExternalId, opt =>
                opt.MapFrom(src => src.ExternalId));
    }
}