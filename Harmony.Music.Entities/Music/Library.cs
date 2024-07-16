using System.ComponentModel.DataAnnotations;
using Harmony.Music.Shared.Helpers;

namespace Harmony.Music.Entities.Music;

public class Library
{
    [MaxLength(45)]
    public string Id { get; set; } = RandomHelper.RandomString(45);

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    
    public string? Path { get; set; }
    public bool HasBeenProcessed { get; set; } = false;

    public bool ArtistImages { get; set; } = false; // download artist images
    public bool AlbumImages { get; set; } = false; // get album images
    public bool AlbumColors { get; set; } = false; // get album colors

    public long? ArtistId { get; set; }
    public long? AlbumId { get; set; }

    public virtual Artist? Artist { get; set; }
    public virtual Album? Album { get; set; }
}