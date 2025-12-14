using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VideoContentReviews.BL.Auth;
using VideoContentReviews.BL.Auth.Entities;
using VideoContentReviews.BL.Auth.Validator.Users;
using VideoContentReviews.BL.User.Exception;
using VideoContentReviews.Service.Controllers.Authorization.DTOs;
using VideoContentReviews.Service.Controllers.Users.DTOs.Requests;
using VideoContentReviews.Service.Controllers.Users.DTOs.Responses;

namespace VideoContentReviews.Service.Controllers.Authorization;


[ApiController]
[Route("[controller]")]
public class AuthorizationController(IAuthProvider authorizationProvider, IMapper mapper)
    : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUser([FromQuery] RegisterUserRequest request)
    {
        var registerModel = mapper.Map<RegisterUserModel>(request);
        var userModel = await authorizationProvider.RegisterUserAsync(registerModel);
        return Ok(mapper.Map<UserResponse>(userModel));
    }

    [HttpGet]
    [Route("login")]
    public async Task<IActionResult> LoginUser([FromQuery] AuthorizeUserRequest request)
    {
        var authorizeModel = mapper.Map<AuthorizeUserModel>(request);
        var tokens = await authorizationProvider.AuthorizeUserAsync(authorizeModel);
        return Ok(tokens);
    }
    
    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> RefreshToken([FromQuery] RefreshTokenRequest request)
    {
        var refreshToken = await authorizationProvider.RefreshTokenAsync(request.RefreshToken);
        return Ok(refreshToken);
    }
}