using Harmony.Music.Contracts.Interfaces;

namespace Harmony.Music.Contracts.Manager;

public interface IRepositoryManager
{
    ISongRepository SongRepository { get; }

    ILibraryRepository LibraryRepository { get; }

    IArtistRepository ArtistRepository { get; }

    IAlbumRepository AlbumRepository { get; }

    void Save();
}