using VideoContentReviews.BL.Users.Entities;

namespace VideoContentReviews.Service.Controllers.Users.DTOs.Responses;

public class UsersListResponse
{
    public List<UserModel> Users { get; set; }
}