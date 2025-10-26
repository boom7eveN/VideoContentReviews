using System.ComponentModel.DataAnnotations.Schema;

namespace VideoContentReviews.DataAccess.Entities;
[Table("TypesOfContent")] 
public class TypeOfContentEntity : BaseEntity
{
    public String Title { get; set; }

    public virtual ICollection<VideoContentEntity> VideoContents { get; set; }
}