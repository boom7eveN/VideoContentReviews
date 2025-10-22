namespace VideoContentReviews.DataAccess.Entities;

public class TypeOfContentEntity : BaseEntity
{
    public String Title { get; set; }

    public virtual ICollection<VideoContentEntity> VideoContents { get; set; }
}