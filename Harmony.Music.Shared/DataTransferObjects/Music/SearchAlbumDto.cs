namespace Harmony.Music.Shared.DataTransferObjects.Music;

public class SearchAlbumDto
{
    public string? Title { get; set; }
    public long? ArtistId { get; set; }
    public bool? HasArtwork { get; set; }
}