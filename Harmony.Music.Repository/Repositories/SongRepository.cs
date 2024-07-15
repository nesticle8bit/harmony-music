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

        if (search.AudioBitrate.HasValue)
            query = query.AsEnumerable()
                .Where(x => x.MediaProperties != null && x.MediaProperties.AudioBitrate == search.AudioBitrate.Value)
                .AsQueryable();

        if (!string.IsNullOrEmpty(search.Path))
            query = query.AsEnumerable()
                .Where(x => x.MediaProperties != null && x.MediaProperties.Path == search.Path)
                .AsQueryable();

        return query;
    }

    public void CreateSong(Song song) => Create(song);
}