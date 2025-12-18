using VideoContentReviews.DataAccess.Entities.Primitives;

namespace VideoContentReviews.BL.Features.Images.DTOs;

public class CreateImageModel
{
    public string FileName { get; set; }
    public ImageFormat FileExtension { get; set; }
}