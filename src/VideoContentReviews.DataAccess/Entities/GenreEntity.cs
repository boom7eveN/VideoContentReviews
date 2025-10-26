using System.ComponentModel.DataAnnotations.Schema;

namespace VideoContentReviews.DataAccess.Entities;
[Table("Genres")] 
public class GenreEntity : BaseEntity
{
    public String Title { get; set; }
    
    public virtual ICollection<VideoContentGenreEntity> VideoContentsGenres { get; set; }
}