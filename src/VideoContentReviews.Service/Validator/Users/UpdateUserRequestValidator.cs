using FluentValidation;
using VideoContentReviews.Service.Controllers.Users.Entities;

namespace VideoContentReviews.Service.Validator.Users;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email address is invalid")
            .MaximumLength(255)
            .WithMessage("Email must be less than 255 characters");

        RuleFor(x => x.UserName)
            .MaximumLength(50)
            .WithMessage("Login must be less than 50 characters");
        

    }
}