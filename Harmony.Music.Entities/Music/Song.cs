using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Harmony.Music.Entities.Base;
using Harmony.Music.Shared.DataTransferObjects;

namespace Harmony.Music.Entities.Music;

public class Song : BaseEntity
{
    [MaxLength(45)]
    public string? LibraryId { get; set; }
    
    public long? AlbumId { get; set; }
    public virtual Album? Album { get; set; }

    public int Track { get; set; }
    
    [MaxLength(400)]
    public string? Name { get; set; }
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    [MaxLength(40)]
    public string? Mimetype { get; set; }
    public bool PossiblyCorrupt { get; set; }
    
    public string? Lyrics { get; set; }

    [Column(TypeName = "jsonb")]
    public MediaPropertyDto? MediaProperties { get; set; }
}