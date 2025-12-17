using FluentValidation;
using VideoContentReviews.BL.Auth.Entities;

namespace VideoContentReviews.BL.Auth.Validators;

public class RegisterUserModelValidator : AbstractValidator<RegisterUserModel>
{
    public RegisterUserModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email address is required")
            .EmailAddress()
            .WithMessage("Email address is invalid")
            .MaximumLength(255)
            .WithMessage("Email must be less than 255 characters");


        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("Login is required")
            .MaximumLength(50)
            .WithMessage("Login must be less than 50 characters");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MaximumLength(50)
            .WithMessage("Password must be less than 255 characters");
    }
}