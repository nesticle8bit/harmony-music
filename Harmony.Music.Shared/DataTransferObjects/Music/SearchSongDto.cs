namespace Harmony.Music.Shared.DataTransferObjects.Music;

public class SearchSongDto
{
    public long? AlbumId { get; set; }
    public string? Name { get; set; }
    public int? AudioBitrate { get; set; }
    public string? Path { get; set; }
}