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

    public IQueryable<Song> SearchSongs(SearchSongDto? search, bool trackChanges)
    {
        var query = FindAll(trackChanges);

        if (!string.IsNullOrEmpty(search?.Name))
            query = query.Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Trim() == search.Name.ToLower().Trim());

        if (search != null)
        {
            if (search.Id.HasValue)
                query = query.Where(x => x.Id == search.Id.Value);

            if (search.AlbumId.HasValue)
                query = query.Where(x => x.AlbumId == search.AlbumId.Value);

            if (!string.IsNullOrEmpty(search.LibraryId))
                query = query.Where(x => x.LibraryId != null && x.LibraryId == search.LibraryId);

            if (search.AudioBitrate.HasValue)
                query = query.AsEnumerable()
                    .Where(x => x.MediaProperties != null && x.MediaProperties.AudioBitrate == search.AudioBitrate.Value)
                    .AsQueryable();

            if (!string.IsNullOrEmpty(search.Path))
                query = query.AsEnumerable()
                    .Where(x => x.MediaProperties != null && x.MediaProperties.Path == search.Path)
                    .AsQueryable();

            if (!string.IsNullOrEmpty(search.Hash))
                query = query.Where(x => x.Hash == search.Hash);
        }

        return query;
    }

    public void CreateSong(Song song) => Create(song);

    public void UpdateSong(Song song) => Update(song);
}