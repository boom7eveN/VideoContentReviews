using VideoContentReviews.BL.Images.Entities;

namespace VideoContentReviews.BL.Images.Managers;

public interface IImageManager
{
    Task<ImageModel> CreateImageAsync(CreateImageModel model);
}