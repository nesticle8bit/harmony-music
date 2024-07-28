using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Harmony.Music.Entities.Base;
using Harmony.Music.Shared.DataTransferObjects.Music;
using Harmony.Music.Shared.Enums;

namespace Harmony.Music.Entities.Music;

public class Album : BaseEntity
{
    [Column(TypeName = "jsonb")]
    public List<long>? Artists { get; set; }
    
    public string Hash { get; set; }
    
    [MaxLength(50)]
    public string? Artwork { get; set; }
    
    [MaxLength(400)]
    public string? Title { get; set; }

    public int Disc { get; set; }
    
    public AlbumTypesEnum Type { get; set; }
    
    public int Year { get; set; }

    [MaxLength(50)]
    public string? MusicBrainzDiscId { get; set; }

    [Column(TypeName = "jsonb")]
    public List<string>? Genres { get; set; }

    public virtual ICollection<Song>? Songs { get; set; } = new HashSet<Song>();
}