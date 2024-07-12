using Harmony.Music.Entities.Music;

namespace Harmony.Music.Contracts.Interfaces;

public interface IMusicRepository
{
    Song? GetSongByUniqueId(string uniqueId, bool trackChanges);
}