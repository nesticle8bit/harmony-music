using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Harmony.Music.Entities.Base;

namespace Harmony.Music.Entities.Music;

public class Artist : BaseEntity
{
    public string Hash { get; set; }
    
    [MaxLength(50)]
    public string? Image { get; set; }
    
    [MaxLength(1000)]
    public string? Name { get; set; }
    public string? Bio { get; set; }
    
    [MaxLength(50)]
    public string? FoundedIn { get; set; }

    [Column(TypeName = "jsonb")]
    public List<string>? Tags { get; set; }
    
    // TODO: Social network profiles list
}