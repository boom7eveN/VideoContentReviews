using FluentValidation;
using VideoContentReviews.BL.VideoContent.Entities;

namespace VideoContentReviews.BL.VideoContent.Validators;

public class UpdateVideoContentModelValidator : AbstractValidator<UpdateVideoContentModel>
{
    public UpdateVideoContentModelValidator()
    {
        When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
        {
            RuleFor(x => x.Name)
                .Length(2, 200).WithMessage("Name must be between 2 and 200 characters");
        });
        
        When(x => x.YearOfRelease.HasValue, () =>
        {
            RuleFor(x => x.YearOfRelease)
                .InclusiveBetween(1900, DateTime.UtcNow.Year + 5)
                .WithMessage($"Year must be between 1900 and {DateTime.UtcNow.Year + 5}");
        });

        When(x => !string.IsNullOrWhiteSpace(x.Description), () =>
        {
            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters");
        });
    }
}