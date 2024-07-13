using Harmony.Music.Contracts.Interfaces;
using Harmony.Music.Contracts.Manager;
using Harmony.Music.Repository.Repositories;

namespace Harmony.Music.Repository.RepositoryManager;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;

    private readonly Lazy<ISongRepository> _songRepository;
    private readonly Lazy<ILibraryRepository> _libraryRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;

        _songRepository = new Lazy<ISongRepository>(() => new SongRepository(repositoryContext));
        _libraryRepository = new Lazy<ILibraryRepository>(() => new LibraryRepository(repositoryContext));
    }

    public ISongRepository SongRepository => _songRepository.Value;
    public ILibraryRepository LibraryRepository => _libraryRepository.Value;
    
    public void Save() => _repositoryContext.SaveChanges();
}