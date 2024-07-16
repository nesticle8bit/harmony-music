using Harmony.Music.ServiceContracts.Services;

namespace Harmony.Music.ServiceContracts.Manager;

public interface IServiceManager
{
    IMusicService MusicService { get; }
    IPlaylistService PlaylistService { get; }
}