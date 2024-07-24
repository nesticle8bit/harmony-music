using Harmony.Music.Contracts.Interfaces;
using Harmony.Music.Contracts.Manager;
using Harmony.Music.Repository.Repositories;

namespace Harmony.Music.Repository.RepositoryManager;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;

    private readonly Lazy<ISongRepository> _songRepository;
    private readonly Lazy<ILibraryRepository> _libraryRepository;
    private readonly Lazy<IArtistRepository> _artistRepository;
    private readonly Lazy<IAlbumRepository> _albumRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;

        _songRepository = new Lazy<ISongRepository>(() => new SongRepository(repositoryContext));
        _libraryRepository = new Lazy<ILibraryRepository>(() => new LibraryRepository(repositoryContext));
        _artistRepository = new Lazy<IArtistRepository>(() => new ArtistRepository(repositoryContext));
        _albumRepository = new Lazy<IAlbumRepository>(() => new AlbumRepository(repositoryContext));
    }

    public ISongRepository SongRepository => _songRepository.Value;
    public ILibraryRepository LibraryRepository => _libraryRepository.Value;
    public IArtistRepository ArtistRepository => _artistRepository.Value;
    public IAlbumRepository AlbumRepository => _albumRepository.Value;

    public void Save() => _repositoryContext.SaveChanges();
}