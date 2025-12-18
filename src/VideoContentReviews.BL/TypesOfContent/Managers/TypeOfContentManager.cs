using AutoMapper;
using VideoContentReviews.BL.Exceptions;
using VideoContentReviews.BL.TypesOfContent.Entities;
using VideoContentReviews.BL.TypesOfContent.Validators;
using VideoContentReviews.DataAccess.Entities;
using VideoContentReviews.DataAccess.Repositories;

namespace VideoContentReviews.BL.TypesOfContent.Managers;

public class TypeOfContentManager : ITypeOfContentManager
{
    private readonly IRepository<TypeOfContentEntity> _contentRepository;
    private readonly IMapper _mapper;

    public TypeOfContentManager(IRepository<TypeOfContentEntity> contentRepository, IMapper mapper)
    {
        _contentRepository = contentRepository;
        _mapper = mapper;
    }


    public async Task<TypeOfContentModel> CreateTypeOfContentAsync(CreateTypeOfContentModel model)
    {
        var validator = new CreateTypeOfContentModelValidator();
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            throw new BusinessLogicException(BLResultCode.ValidationError,
                string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var sameTypes = await _contentRepository
            .GetAllAsync(x => x.Title == model.Title);
        if (sameTypes.Any())
        {
            throw new BusinessLogicException(BLResultCode.TypeOfContentAlreadyExists);
        }

        var entity = _mapper.Map<TypeOfContentEntity>(model);
        entity = await _contentRepository.SaveAsync(entity);
        return _mapper.Map<TypeOfContentModel>(entity);
    }
}