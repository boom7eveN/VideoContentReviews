using VideoContentReviews.DataAccess.Entities.Primitives;

namespace VideoContentReviews.BL.Image.Entities;

public class CreateImageModel
{
    public string FileName { get; set; }
    public ImageFormat FileExtension { get; set; }
}