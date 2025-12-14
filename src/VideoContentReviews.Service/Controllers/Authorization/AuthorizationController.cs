using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VideoContentReviews.BL.Auth;
using VideoContentReviews.BL.Auth.Entities;
using VideoContentReviews.BL.User.Exception;
using VideoContentReviews.Service.Controllers.Authorization.Entities;
using VideoContentReviews.Service.Controllers.Users.Entities;
using VideoContentReviews.Service.Validator.Users;

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
        try
        {
            var validationResult = await new RegisterUserRequestValidator().ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage);
                var stringBuilder = new StringBuilder();
                foreach (var error in errors)
                    stringBuilder.AppendLine(error);
                return BadRequest(errors);
            }
        
            var registerModel = mapper.Map<RegisterUserModel>(request);
            var userModel = await authorizationProvider.RegisterUserAsync(registerModel);
            return Ok(new UsersListResponse
            {
                Users = [userModel]
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("login")]
    public async Task<IActionResult> LoginUser([FromQuery] AuthorizeUserRequest request)
    {
        try
        {
            var authorizeModel = mapper.Map<AuthorizeUserModel>(request);
            var tokens = await authorizationProvider.AuthorizeUserAsync(authorizeModel);

            return Ok(tokens);
        }
        catch (BusinessLogicException e) when (e.ResultCode == ResultCode.UserNotFound)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}