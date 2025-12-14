using VideoContentReviews.BL.User.Entities;

namespace VideoContentReviews.Service.Controllers.Users.DTOs.Requests;

public class UsersListResponse
{
    public List<UserModel> Users { get; set; } 
}