namespace Harmony.Music.Shared.DataTransferObjects.Music;

public record AlbumInfoDto
{
    public string? Hash { get; set; }
    public string? Artwork { get; set; }
    public string? Title { get; set; }
    public int Year { get; set; }
}