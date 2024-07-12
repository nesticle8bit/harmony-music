using Harmony.Music.Contracts.Interfaces;

namespace Harmony.Music.Contracts.Manager;

public interface IRepositoryManager
{
    IMusicRepository MusicRepository { get; }
}