using System.ComponentModel.DataAnnotations.Schema;

namespace VideoContentReviews.DataAccess.Entities;
[Table("Images")] 
public class ImageEntity : BaseEntity
{
    public String FileName { get; set; }
    public String FileExtension { get; set; }
    public String RelativePath { get; set; }
    
    public virtual ICollection<VideoContentEntity> VideoContents { get; set; }
    
}