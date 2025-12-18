using FluentValidation;
using VideoContentReviews.BL.Directors.Entities;

namespace VideoContentReviews.BL.Directors.Validators;

public class CreateDirectorModelValidator : AbstractValidator<CreateDirectorModel>
{
    public CreateDirectorModelValidator()
    {
        var nameRegex = @"^[\p{L}]+([\s'-][\p{L}]+)*$";

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .Length(2, 50).WithMessage("First name must be between 2 and 50 characters")
            .Matches(nameRegex).WithMessage("First name contains invalid characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters")
            .Matches(nameRegex).WithMessage("Last name contains invalid characters");

        RuleFor(x => x.Patronymic)
            .Length(2, 50).WithMessage("Patronymic must be between 2 and 50 characters")
            .Matches(nameRegex).WithMessage("Patronymic contains invalid characters")
            .When(x => !string.IsNullOrWhiteSpace(x.Patronymic));
    }
}