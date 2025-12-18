using FluentValidation;
using VideoContentReviews.BL.Features.VideoContent.DTOs;

namespace VideoContentReviews.BL.Features.VideoContent.Validators;

public class CreateVideoContentModelValidator : AbstractValidator<CreateVideoContentModel>
{
    public CreateVideoContentModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 200).WithMessage("Name must be between 2 and 200 characters");

        RuleFor(x => x.YearOfRelease)
            .NotEmpty().WithMessage("Year of release is required")
            .InclusiveBetween(1900, DateTime.UtcNow.Year + 5)
            .WithMessage($"Year must be between 1900 and {DateTime.UtcNow.Year + 5}");

        RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters")
            .When(x => !string.IsNullOrEmpty(x.Description));


        RuleFor(x => x.TypeOfContentExternalId)
            .NotEmpty().WithMessage("Type of content is required")
            .NotEqual(Guid.Empty).WithMessage("Invalid type of content ID");

        RuleFor(x => x.DirectorExternalId)
            .NotEmpty().WithMessage("Director is required")
            .NotEqual(Guid.Empty).WithMessage("Invalid director ID");

        RuleFor(x => x.ImageExternalId)
            .NotEmpty().WithMessage("Image is required")
            .NotEqual(Guid.Empty).WithMessage("Invalid image ID");

        RuleFor(x => x.GenreExternalIds)
            .NotNull().WithMessage("Genres cannot be null")
            .Must(ids => ids == null || ids.Count <= 10)
            .WithMessage("Cannot have more than 10 genres")
            .Must(ids => ids == null || ids.All(id => id != Guid.Empty))
            .WithMessage("Genre list contains invalid IDs")
            .When(x => true);
    }
}