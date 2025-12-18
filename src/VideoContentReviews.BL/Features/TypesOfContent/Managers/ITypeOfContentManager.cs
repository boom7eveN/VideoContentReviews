using VideoContentReviews.BL.Features.TypesOfContent.DTOs;

namespace VideoContentReviews.BL.Features.TypesOfContent.Managers;

public interface ITypeOfContentManager
{
    Task<TypeOfContentModel> CreateTypeOfContentAsync(CreateTypeOfContentModel model);
}