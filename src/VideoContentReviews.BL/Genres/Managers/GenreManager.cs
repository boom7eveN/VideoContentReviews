using AutoMapper;
using VideoContentReviews.BL.Exceptions;
using VideoContentReviews.BL.Genres.Entities;
using VideoContentReviews.DataAccess.Entities;
using VideoContentReviews.DataAccess.Repositories;

namespace VideoContentReviews.BL.Genres.Managers;

public class GenreManager(IRepository<GenreEntity> genreRepository, IMapper mapper) : IGenreManager
{
    public async Task<GenreModel> CreateGenreAsync(CreateGenreModel model)
    {
        // var validator = new CreateGenreModelValidator();
        // var validationResult = await validator.ValidateAsync(model);
        //
        // if (!validationResult.IsValid)
        // {
        //     throw new BusinessLogicException(ResultCode.ValidationError,
        //         string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
        // }
        
        var existing = await genreRepository.GetAllAsync(g => g.Title == model.Title);
        if (existing.Any())
        {
            throw new BusinessLogicException(BLResultCode.GenreAlreadyExists);
        }

        var entity = mapper.Map<GenreEntity>(model);
        entity = await genreRepository.SaveAsync(entity);
        return mapper.Map<GenreModel>(entity);
    }
}