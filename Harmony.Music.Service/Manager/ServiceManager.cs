using AutoMapper;
using Harmony.Music.Contracts.Manager;
using Harmony.Music.Service.Services;
using Harmony.Music.ServiceContracts.Manager;
using Harmony.Music.ServiceContracts.Services;

namespace Harmony.Music.Service.Manager;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IMusicService> _musicService;
    private readonly Lazy<IPlaylistService> _playlistService;
    private readonly Lazy<ILastfmService> _lastfmService;

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _musicService = new Lazy<IMusicService>(() => new MusicService(repositoryManager, mapper));
        _playlistService = new Lazy<IPlaylistService>(() => new PlaylistService(repositoryManager, mapper));
        _lastfmService = new Lazy<ILastfmService>(() => new LastfmService(mapper));
    }

    public IMusicService MusicService => _musicService.Value;
    public IPlaylistService PlaylistService => _playlistService.Value;
    public ILastfmService LastfmService => _lastfmService.Value;
}