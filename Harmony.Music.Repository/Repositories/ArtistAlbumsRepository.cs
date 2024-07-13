using Harmony.Music.Contracts.Interfaces;
using Harmony.Music.Entities.Music;
using Harmony.Music.Shared.DataTransferObjects.Music;
using Harmony.Music.Repository.RepositoryBase;

namespace Harmony.Music.Repository.Repositories;

public class ArtistAlbumsRepository : RepositoryBase<ArtistAlbums>, IArtistAlbumsRepository
{
    public ArtistAlbumsRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<ArtistAlbums> SearchArtistAlbums(SearchArtistAlbumsDto? search, bool trackChanges)
    {
        var query = FindAll(trackChanges);

        if (search != null)
        {
            if (search.AlbumId.HasValue)
                query = query.Where(x => x.AlbumId == search.AlbumId.Value);

            if (search.ArtistId.HasValue)
                query = query.Where(x => x.ArtistId == search.ArtistId.Value);
        }

        return query;
    }

    public void CreateArtistAlbums(ArtistAlbums artistAlbums) => Create(artistAlbums);
}