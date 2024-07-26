using System.Text.Json.Serialization;

namespace Harmony.Music.Shared.DataTransferObjects.Lastfm;

public class LastfmTopTracksResponse
{
    [JsonPropertyName("toptracks")]
    public LastfmTopTracks? TopTracks { get; set; }
}

public class LastfmTopTracks
{
    [JsonPropertyName("track")]
    public List<LastfmTrackDto>? Track { get; set; }
}

public class LastfmTrackDto
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("playcount")]
    public string? PlayCount { get; set; }
}

