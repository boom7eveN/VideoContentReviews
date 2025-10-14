namespace VideoContentReviews.DataAccess.Entities;

public class Image : BaseEntity
{
    public String FileName { get; set; }
    public String FileExtension { get; set; }
    public String RelativePath { get; set; }
    
    public virtual ICollection<VideoContent> VideoContents { get; set; }
    
}