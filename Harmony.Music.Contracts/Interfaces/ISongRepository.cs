using Harmony.Music.Entities.Music;
using Harmony.Music.Shared.DataTransferObjects.Music;

namespace Harmony.Music.Contracts.Interfaces;

public interface ISongRepository
{
    IQueryable<Song> SearchSongs(SearchSongDto search, bool trackChanges);
    
    void CreateSong(Song song);
}