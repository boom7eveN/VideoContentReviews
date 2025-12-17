using FluentValidation;
using VideoContentReviews.BL.TypeOfContent.Entities;

namespace VideoContentReviews.BL.TypeOfContent.Validators;

public class CreateTypeOfContentModelValidator : AbstractValidator<CreateTypeOfContentModel>
{
    public CreateTypeOfContentModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .Length(2, 50).WithMessage("Title must be between 2 and 50 characters");
    }
}