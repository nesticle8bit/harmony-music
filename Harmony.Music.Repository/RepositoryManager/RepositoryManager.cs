using Harmony.Music.Contracts.Interfaces;
using Harmony.Music.Contracts.Manager;
using Harmony.Music.Repository.Repositories;

namespace Harmony.Music.Repository.RepositoryManager;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;

    private readonly Lazy<IMusicRepository> _musicRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;

        _musicRepository = new Lazy<IMusicRepository>(() => new MusicRepository(repositoryContext));
    }
    
    public IMusicRepository MusicRepository => _musicRepository.Value;
}