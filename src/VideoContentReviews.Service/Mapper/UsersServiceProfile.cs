using AutoMapper;
using VideoContentReviews.BL.Auth.Entities;
using VideoContentReviews.BL.User.Entities;
using VideoContentReviews.Service.Controllers.Authorization.Entities;
using VideoContentReviews.Service.Controllers.Users.Entities;

namespace VideoContentReviews.Service.Mapper;

public class UsersServiceProfile : Profile
{
    public UsersServiceProfile()
    {
        CreateMap<RegisterUserRequest, RegisterUserModel>()
            .ForMember(dest => dest.Role, opt => opt.Ignore());
        CreateMap<UpdateUserRequest, UpdateUserModel>();
        CreateMap<UserFilter, UserModelFilter>();
        CreateMap<AuthorizeUserRequest, AuthorizeUserModel>();
    }
}