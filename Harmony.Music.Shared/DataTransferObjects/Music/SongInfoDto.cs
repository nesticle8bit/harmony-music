namespace Harmony.Music.Shared.DataTransferObjects.Music;

public record SongInfoDto
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? Mimetype { get; init; }

    public MediaPropertyDto? MediaProperties { get; init; }
    public virtual AlbumInfoDto? Album { get; init; }
    public virtual ArtistInfoDto? Artist { get; init; }
}