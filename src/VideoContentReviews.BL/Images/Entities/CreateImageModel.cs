using VideoContentReviews.DataAccess.Entities.Primitives;

namespace VideoContentReviews.BL.Images.Entities;

public class CreateImageModel
{
    public string FileName { get; set; }
    public ImageFormat FileExtension { get; set; }
}