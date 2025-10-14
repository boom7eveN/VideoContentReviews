namespace VideoContentReviews.DataAccess.Entities;

public class Director : BaseEntity
{
    public String FirstName { get; set; }
    public String MiddleName { get; set; }
    public String Patronymic { get; set; }
    
    public virtual ICollection<VideoContent> VideoContents { get; set; }
}