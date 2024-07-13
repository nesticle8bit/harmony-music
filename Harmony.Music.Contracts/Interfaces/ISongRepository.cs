using Harmony.Music.Entities.Music;

namespace Harmony.Music.Contracts.Interfaces;

public interface ISongRepository
{
    Song? GetSongByUniqueId(string uniqueId, bool trackChanges);
    
}