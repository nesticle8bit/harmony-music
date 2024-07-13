namespace Harmony.Music.Shared.DataTransferObjects.Music;

public class ExtractMusicMetadataReportDto
{
    public int SongsProcessed { get; set; }
    public int SongsImported { get; set; }
    public int SongsRemoved { get; set; }

    public List<LibrarySyncErrorsDto>? Errors { get; set; } = [];
}