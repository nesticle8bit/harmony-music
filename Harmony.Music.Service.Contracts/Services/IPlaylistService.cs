using Harmony.Music.Shared.DataTransferObjects.Playlists;

namespace Harmony.Music.ServiceContracts.Services;

public interface IPlaylistService
{
    List<PlaylistDto> GetRecentlyAdded();
}