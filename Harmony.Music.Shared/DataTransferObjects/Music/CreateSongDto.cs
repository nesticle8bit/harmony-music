namespace Harmony.Music.Shared.DataTransferObjects.Music;

public class CreateSongDto
{
    public ArtistInfoDto Artist { get; set; } = new ArtistInfoDto();
    public AlbumInfoDto Album { get; set; } = new AlbumInfoDto();

    public List<long>? Artists { get; set; }
    
    public string? FilePath { get; set; }
    public long? AlbumId { get; set; }
    public string? LibraryId { get; set; }
    public MediaMetadataDto? Metadata { get; set; }
}