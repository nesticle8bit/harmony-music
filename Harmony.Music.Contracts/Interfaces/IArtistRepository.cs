using Harmony.Music.Entities.Music;
using Harmony.Music.Shared.DataTransferObjects.Music;

namespace Harmony.Music.Contracts.Interfaces;

public interface IArtistRepository
{
    IQueryable<Artist> SearchArtist(SearchArtistDto? search, bool trackChanges);

    void CreateArtist(Artist artist);
}