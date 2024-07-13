using Harmony.Music.Contracts.Interfaces;
using Harmony.Music.Entities.Music;
using Harmony.Music.Shared.DataTransferObjects.Music;
using Harmony.Music.Repository.RepositoryBase;

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

        return query;
    }

    public void CreateAlbum(Album album) => Create(album);
}