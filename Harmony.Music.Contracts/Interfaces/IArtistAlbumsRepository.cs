using Harmony.Music.Entities.Music;
using Harmony.Music.Shared.DataTransferObjects.Music;

namespace Harmony.Music.Contracts.Interfaces;

public interface IArtistAlbumsRepository
{
    IQueryable<ArtistAlbums> SearchArtistAlbums(SearchArtistAlbumsDto? search, bool trackChanges);

    void CreateArtistAlbums(ArtistAlbums artistAlbums);
}