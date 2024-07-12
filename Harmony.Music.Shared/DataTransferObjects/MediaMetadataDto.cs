namespace Harmony.Music.Shared.DataTransferObjects;

public class MediaMetadataDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Mimetype { get; set; }
    public bool PossiblyCorrupt { get; set; }

    public TrackPropertyDto? TrackProperties { get; set; }
    public MediaPropertyDto? MediaProperties { get; set; }
}