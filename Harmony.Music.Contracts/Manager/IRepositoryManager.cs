using Harmony.Music.Contracts.Interfaces;

namespace Harmony.Music.Contracts.Manager;

public interface IRepositoryManager
{
    ISongRepository SongRepository { get; }

    ILibraryRepository LibraryRepository { get; }
}