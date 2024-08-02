namespace Harmony.Music.Shared.DataTransferObjects.Music;

public class SearchAlbumDto
{
    public long? Id { get; set; }
    public string? Title { get; set; }
    public long? ArtistId { get; set; }
    public bool? HasArtwork { get; set; }
}