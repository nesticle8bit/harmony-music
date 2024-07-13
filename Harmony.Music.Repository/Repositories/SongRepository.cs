using Harmony.Music.Contracts.Interfaces;
using Harmony.Music.Entities.Music;
using Harmony.Music.Repository.RepositoryBase;
using Harmony.Music.Shared.DataTransferObjects.Music;

namespace Harmony.Music.Repository.Repositories;

public class SongRepository : RepositoryBase<Song>, ISongRepository
{
    public SongRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<Song> SearchSongs(SearchSongDto search, bool trackChanges)
    {
        var query = FindAll(trackChanges);

        if (!string.IsNullOrEmpty(search?.Name))
            query = query.Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Trim() == search.Name.ToLower().Trim());
        
        if (search.AlbumId.HasValue)
            query = query.Where(x => x.AlbumId == search.AlbumId.Value);

        return query;
    }
    
    public void CreateSong(Song song) => Create(song);
}