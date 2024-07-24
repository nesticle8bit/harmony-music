namespace Harmony.Music.Shared.DataTransferObjects.Music;

public record ArtistInfoDto
{
    public string? Hash { get; init; }
    public string? Name { get; init; }
}