using Harmony.Music.Shared.DataTransferObjects.Music;

namespace Harmony.Music.Shared.DataTransferObjects.Playlists;

public class PlaylistDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public ArtistInfoDto? Artist { get; set; }
    public DateTime? DateCreated { get; set; }
}