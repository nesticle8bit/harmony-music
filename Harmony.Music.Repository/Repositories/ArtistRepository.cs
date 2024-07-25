using Harmony.Music.Contracts.Interfaces;
using Harmony.Music.Entities.Music;
using Harmony.Music.Repository.RepositoryBase;
using Harmony.Music.Shared.DataTransferObjects.Music;

namespace Harmony.Music.Repository.Repositories;

public class ArtistRepository : RepositoryBase<Artist>, IArtistRepository
{
    public ArtistRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<Artist> SearchArtist(SearchArtistDto? search, bool trackChanges)
    {
        var query = FindAll(trackChanges);

        if (!string.IsNullOrEmpty(search?.Name))
            query = query.Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Trim() == search.Name.ToLower().Trim());

        if (!string.IsNullOrEmpty(search?.Hash))
            query = query.Where(x => !string.IsNullOrEmpty(x.Hash) && x.Hash.Equals(search.Hash.Trim()));

        return query;
    }

    public void CreateArtist(Artist artist) => Create(artist);
}