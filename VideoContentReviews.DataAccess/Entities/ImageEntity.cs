using System.ComponentModel.DataAnnotations.Schema;
using VideoContentReviews.DataAccess.Entities.Primitives;

namespace VideoContentReviews.DataAccess.Entities;
[Table("Images")] 
public class ImageEntity : BaseEntity
{
    public string FileName { get; set; }
    public ImageFormat FileExtension { get; set; }
    
    public virtual ICollection<VideoContentEntity> VideoContents { get; set; }
    
}