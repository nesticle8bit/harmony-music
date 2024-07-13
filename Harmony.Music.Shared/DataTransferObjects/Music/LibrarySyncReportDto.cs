namespace Harmony.Music.Shared.DataTransferObjects.Music;

public class LibrarySyncReportDto
{
    public int SongsFound { get; set; }
    public int SongsImported { get; set; }
    public List<LibrarySyncErrorsDto> Errors { get; set; } = [];
}

public class LibrarySyncErrorsDto
{
    public string? Error { get; set; }
    public string? StackTrace { get; set; }
}