using VideoContentReviews.DataAccess.Entities.Primitives;

namespace VideoContentReviews.BL.Images.Entities;

public class ImageModel
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    public string FileName { get; set; }
    public ImageFormat FileExtension { get; set; }
    public string FullPath { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
}