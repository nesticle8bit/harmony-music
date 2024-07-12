using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Harmony.Music.Entities.Base;

namespace Harmony.Music.Entities.Music;

public class Artist : BaseEntity
{
    [MaxLength(50)]
    public string? Image { get; set; }
    
    [MaxLength(400)]
    public string? Name { get; set; }
    public string? Bio { get; set; }
    public string? FoundedIn { get; set; }

    [Column(TypeName = "jsonb")]
    public List<string>? Tags { get; set; }
    
    [MaxLength(150)]
    public string? MetalArchivesUrl { get; set; }

    public virtual ICollection<ArtistAlbums> ArtistAlbums { get; set; } = new HashSet<ArtistAlbums>();
}