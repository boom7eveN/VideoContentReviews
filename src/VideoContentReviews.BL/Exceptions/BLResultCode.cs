using System.ComponentModel;

namespace VideoContentReviews.BL.Exceptions;

public enum BLResultCode
{
    [Description("User not found.")]
    UserNotFound = 001,
    
    [Description("User already exists.")]
    UserAlreadyExists = 002,

    [Description("Email or password is incorrect.")]
    EmailOrPasswordIsIncorrect = 003,
    
    [Description("Identity server error.")]
    IdentityServerError = 004,
    
    [Description("User creation failure.")]
    UserCreationFailure = 005,
    
    [Description("Validation error.")]
    ValidationError = 006,
    
    [Description("Videocontent not found.")]
    VideoContentNotFound = 007,
    
    [Description("Type of content not found.")]
    TypeOfContentNotFound = 008,
    
    [Description("Director not found.")]
    DirectorNotFound = 009,
    
    [Description("Director already exists.")]
    DirectorAlreadyExists = 010,
    
    [Description("Type of content already exists.")]
    TypeOfContentAlreadyExists = 011,
    
    [Description("Image already exists.")]
    ImageAlreadyExists  = 012,
    
    [Description("Genre already exists.")]
    GenreAlreadyExists = 013,
    
    [Description("VideoContent already exists.")]
    VideoContentAlreadyExists = 014,
    [Description("Image not found.")]
    ImageNotFound = 015,
    [Description("Genre not found.")]
    GenreNotFound = 016,
}