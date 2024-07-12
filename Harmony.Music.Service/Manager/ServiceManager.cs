using AutoMapper;
using Harmony.Music.Contracts.Manager;
using Harmony.Music.Service.Services;
using Harmony.Music.ServiceContracts.Manager;
using Harmony.Music.ServiceContracts.Services;

namespace Harmony.Music.Service.Manager;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IMusicService> _musicService;

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _musicService = new Lazy<IMusicService>(() => new MusicService(repositoryManager, mapper));
    }

    public IMusicService MusicService => _musicService.Value;
}