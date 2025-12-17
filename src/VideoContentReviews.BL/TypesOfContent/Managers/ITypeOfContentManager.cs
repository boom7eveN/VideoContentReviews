using VideoContentReviews.BL.TypesOfContent.Entities;

namespace VideoContentReviews.BL.TypesOfContent.Managers;

public interface ITypeOfContentManager
{
    Task<TypeOfContentModel> CreateTypeOfContentAsync(CreateTypeOfContentModel model);
}