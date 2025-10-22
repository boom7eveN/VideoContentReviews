using System.ComponentModel.DataAnnotations.Schema;

namespace VideoContentReviews.DataAccess.Entities;
[Table("VideoContentGenres")] 
public class VideoContentGenreEntity : BaseEntity
{
    public Guid VideoContentId { get; set; }
    public VideoContentEntity VideoContentEntity { get; set; }

    public Guid GenreId { get; set; }
    public GenreEntity GenreEntity { get; set; }

    public DateTime AddedTime { get; set; }
}