using AutoMapper;
using VideoContentReviews.BL.Exception;
using VideoContentReviews.BL.Image.Entities;
using VideoContentReviews.BL.Image.Validators;
using VideoContentReviews.DataAccess.Entities;
using VideoContentReviews.DataAccess.Repositories;

namespace VideoContentReviews.BL.Image.Manager;

public class ImageManager(IRepository<ImageEntity> imageRepository, IMapper mapper) : IImageManager
{
    public async Task<ImageModel> CreateImageAsync(CreateImageModel model)
    {
        var validator = new CreateImageModelValidator();
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            throw new BusinessLogicException(BLResultCode.ValidationError, 
                string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var sameImages = await imageRepository.GetAllAsync(x => x.FileName == model.FileName && x.FileExtension == model.FileExtension);
        if (sameImages.Any())
        {
            throw new BusinessLogicException(BLResultCode.ImageAlreadyExists);
        }
        
        var entity = mapper.Map<ImageEntity>(model);
        entity = await imageRepository.SaveAsync(entity);
        return mapper.Map<ImageModel>(entity);
    }
}