using VideoContentReviews.BL.Image.Entities;

namespace VideoContentReviews.BL.Image.Manager;

public interface IImageManager
{
    Task<ImageModel> CreateImageAsync(CreateImageModel model);
}