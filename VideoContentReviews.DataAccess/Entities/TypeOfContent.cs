namespace VideoContentReviews.DataAccess.Entities;

public class TypeOfContent : BaseEntity
{
    public String Title { get; set; }

    public virtual ICollection<VideoContent> VideoContents { get; set; }
}