using VideoContentReviews.BL.Features.Images.DTOs;

namespace VideoContentReviews.BL.Features.Images.Managers;

public interface IImageManager
{
    Task<ImageModel> CreateImageAsync(CreateImageModel model);
}