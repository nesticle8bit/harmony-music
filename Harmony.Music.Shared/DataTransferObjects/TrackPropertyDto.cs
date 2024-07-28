namespace Harmony.Music.Shared.DataTransferObjects;

public class TrackPropertyDto
{
    public string? Title { get; set; }
    public string? Album { get; set; }
    public uint Year { get; set; }

    public List<string>? Genres { get; set; }
    public List<string>? Artists { get; set; }

    public uint Track { get; set; }
    public uint TrackCount { get; set; }
    public uint Disc { get; set; }
    public uint DiscCount { get; set; }

    public string? Lyrics { get; set; }
}