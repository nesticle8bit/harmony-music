using System.ComponentModel.DataAnnotations.Schema;
using Harmony.Music.Entities.Base;
using Harmony.Music.Shared.DataTransferObjects;

namespace Harmony.Music.Entities.Music;

public class Song : BaseEntity
{
    public long AlbumId { get; set; }

    public int Track { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string Mimetype { get; set; }
    public bool PossiblyCorrupt { get; set; }
    
    public string? Lyrics { get; set; }

    [Column(TypeName = "jsonb")]
    public MediaPropertyDto? MediaProperties { get; set; }

    public virtual Album? Album { get; set; }
}