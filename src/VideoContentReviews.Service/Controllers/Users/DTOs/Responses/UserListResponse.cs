using VideoContentReviews.BL.Features.Users.DTOs;

namespace VideoContentReviews.Service.Controllers.Users.DTOs.Responses;

public class UsersListResponse
{
    public List<UserModel> Users { get; set; }
}