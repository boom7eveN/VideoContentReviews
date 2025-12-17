using VideoContentReviews.BL.TypeOfContent.Entities;

namespace VideoContentReviews.BL.TypeOfContent.Managers;

public interface ITypeOfContentManager
{
    Task<TypeOfContentModel> CreateTypeOfContentAsync(CreateTypeOfContentModel model);
}