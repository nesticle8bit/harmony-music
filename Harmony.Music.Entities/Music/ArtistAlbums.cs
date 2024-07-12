using Harmony.Music.Entities.Base;

namespace Harmony.Music.Entities.Music;

public class ArtistAlbums : BaseEntity
{
    public long ArtistId { get; set; }
    public long AlbumId { get; set; }

    public virtual Artist? Artist { get; set; }
    public virtual Album? Album { get; set; }
}