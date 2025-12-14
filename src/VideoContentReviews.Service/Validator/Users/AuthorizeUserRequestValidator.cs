using FluentValidation;
using VideoContentReviews.Service.Controllers.Authorization.Entities;

namespace VideoContentReviews.Service.Validator.Users;

public class AuthorizeUserRequestValidator : AbstractValidator<AuthorizeUserRequest>
{
    public AuthorizeUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email address is required")
            .EmailAddress()
            .WithMessage("Email address is invalid")
            .MaximumLength(255)
            .WithMessage("Email must be less than 255 characters");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MaximumLength(50)
            .WithMessage("Password must be less than 255 characters");
    }
}