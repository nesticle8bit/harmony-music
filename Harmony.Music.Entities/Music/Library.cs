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

    public virtual ICollection<Song> Songs { get; set; } = new HashSet<Song>();
}