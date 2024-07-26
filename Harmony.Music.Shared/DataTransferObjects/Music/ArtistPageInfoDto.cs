namespace Harmony.Music.Shared.DataTransferObjects.Music;

public record ArtistPageInfoDto : ArtistInfoDto
{
    public string? Bio { get; init; }
    public string? FoundedIn { get; init; }
    public string? Image { get; init; }
    public string? MetalArchivesUrl { get; init; }
    public List<string>? Tags { get; init; }
    public int Songs { get; set; }
    public List<AlbumInfoDto>? Albums { get; init; }
}