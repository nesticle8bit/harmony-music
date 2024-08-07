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

        if (search != null)
        {
            if (search.Id.HasValue)
                query = query.Where(x => x.Id == search.Id.Value);
            
            if (!string.IsNullOrEmpty(search?.Title))
                query = query.Where(x => x.Title != null && x.Title.ToLower().Trim() == search.Title.ToLower().Trim());

            if (search != null && search.ArtistId.HasValue)
            {
                query = query
                    .AsEnumerable()
                    .Where(x => x.Artists != null && x.Artists.Contains(search.ArtistId.Value))
                    .AsQueryable();
            }

            if (search != null && search.HasArtwork.HasValue)
            {
                if (search.HasArtwork.Value)
                    query = query.Where(x => x.Artwork != null);
                else
                    query = query.Where(x => x.Artwork == null);
            }
        }

        return query;
    }

    public void CreateAlbum(Album album) => Create(album);

    public void UpdateAlbum(Album album) => Update(album);
}