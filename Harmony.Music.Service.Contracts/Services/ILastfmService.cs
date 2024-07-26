using Harmony.Music.Shared.DataTransferObjects.Lastfm;

namespace Harmony.Music.ServiceContracts.Services;

public interface ILastfmService
{
    Task<List<LastfmTrackDto>?> GetTopTracks(string? artist);
}