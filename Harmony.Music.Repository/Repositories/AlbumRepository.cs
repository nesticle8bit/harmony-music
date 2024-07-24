using Harmony.Music.Contracts.Interfaces;
using Harmony.Music.Entities.Music;
using Harmony.Music.Shared.DataTransferObjects.Music;
using Harmony.Music.Repository.RepositoryBase;
using Microsoft.EntityFrameworkCore;

namespace Harmony.Music.Repository.Repositories;

public class AlbumRepository : RepositoryBase<Album>, IAlbumRepository
{
    public AlbumRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<Album> SearchAlbums(SearchAlbumDto? search, bool trackChanges)
    {
        var query = FindAll(trackChanges);

        if (!string.IsNullOrEmpty(search?.Title))
            query = query.Where(x => !string.IsNullOrEmpty(x.Title) && x.Title.ToLower().Trim() == search.Title.ToLower().Trim());

        if (search.ArtistId.HasValue)
        {
            query = query
                .Include(x => x.Songs)
                .Where(x => x.Songs != null && x.Songs.Any(aa => aa.ArtistId == search.ArtistId.Value));
        }

        return query;
    }

    public void CreateAlbum(Album album) => Create(album);
}