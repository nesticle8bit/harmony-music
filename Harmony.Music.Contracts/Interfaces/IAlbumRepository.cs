using Harmony.Music.Entities.Music;
using Harmony.Music.Shared.DataTransferObjects.Music;

namespace Harmony.Music.Contracts.Interfaces;

public interface IAlbumRepository
{
    IQueryable<Album> SearchAlbums(SearchAlbumDto? search, bool trackChanges);

    void CreateAlbum(Album album);
    
    void UpdateAlbum(Album album);
}