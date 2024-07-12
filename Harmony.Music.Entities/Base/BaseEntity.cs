using System.ComponentModel.DataAnnotations;

namespace Harmony.Music.Entities.Base;

public class BaseEntity
{
    [Key]
    public long Id { get; set; }

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}