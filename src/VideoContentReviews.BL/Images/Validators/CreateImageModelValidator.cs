using FluentValidation;
using VideoContentReviews.BL.Images.Entities;
using VideoContentReviews.DataAccess.Entities.Primitives;

namespace VideoContentReviews.BL.Images.Validators;

public class CreateImageModelValidator : AbstractValidator<CreateImageModel>
{
    public CreateImageModelValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage("File name is required")
            .MaximumLength(70).WithMessage("File name must not exceed 70 characters")
            .Must(BeValidFileName).WithMessage("Invalid file name");
        
        RuleFor(x => x.FileExtension)
            .IsInEnum().WithMessage("Invalid file extension");
        
        RuleFor(x => x)
            .Must(x => BeValidFullFileName(x.FileName, x.FileExtension))
            .WithMessage("Invalid full file name");
    }
    
    private bool BeValidFileName(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName)) 
            return false;

        var invalidChars = Path.GetInvalidFileNameChars();
        return !fileName.Any(c => invalidChars.Contains(c));
    }
    
    private bool BeValidFullFileName(string fileName, ImageFormat extension)
    {
        var fullName = $"{fileName}.{extension.ToString().ToLower()}";
        return fullName.Length <= 150; 
    }
}