using AutoMapper;
using VideoContentReviews.BL.Directors.Entities;
using VideoContentReviews.BL.Directors.Validators;
using VideoContentReviews.BL.Exceptions;
using VideoContentReviews.DataAccess.Entities;
using VideoContentReviews.DataAccess.Repositories;

namespace VideoContentReviews.BL.Directors.Managers;

public class DirectorManager(IRepository<DirectorEntity> directorsRepository, IMapper mapper)
    : IDirectorManager
{
    public async Task<DirectorModel> CreateDirectorAsync(CreateDirectorModel model)
    {
        var validator = new CreateDirectorModelValidator();
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            throw new BusinessLogicException(BLResultCode.ValidationError, 
                string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var sameDirectors = await directorsRepository.GetAllAsync(x => x.FirstName == model.FirstName && x.LastName == model.LastName);
        if (sameDirectors.Any())
        {
            throw new BusinessLogicException(BLResultCode.DirectorAlreadyExists);
        }
        
        var entity = mapper.Map<DirectorEntity>(model);
        entity = await directorsRepository.SaveAsync(entity);
        return mapper.Map<DirectorModel>(entity);
    }
}