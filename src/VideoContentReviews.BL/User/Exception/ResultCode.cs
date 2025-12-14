using System.ComponentModel;

namespace VideoContentReviews.BL.User.Exception;

public enum ResultCode
{
    [Description("User not found")]
    UserNotFound = 001,
    
    [Description("User already exists.")]
    UserAlreadyExists = 002,

    [Description("Email or password is incorrect.")]
    EmailOrPasswordIsIncorrect = 003,
    
    [Description("Identity server error.")]
    IdentityServerError = 004,
    
    [Description("User creation failure.")]
    UserCreationFailure = 005,
}