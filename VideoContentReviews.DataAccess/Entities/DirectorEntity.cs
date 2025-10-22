namespace VideoContentReviews.DataAccess.Entities;

public class DirectorEntity : BaseEntity
{
    public String FirstName { get; set; }
    public String MiddleName { get; set; }
    public String Patronymic { get; set; }
    
    public virtual ICollection<VideoContentEntity> VideoContents { get; set; }
}