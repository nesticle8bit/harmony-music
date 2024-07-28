using Harmony.Music.Shared.DataTransferObjects.Music;

namespace Harmony.Music.ServiceContracts.Services;

public interface IMusicService
{
    LibrarySyncReportDto SyncLibrary();

    ExtractMusicMetadataReportDto ExtractMusicMetadata();

    int ExtractAlbumsArtwork();

    string GetFilePathBySongId(long? songId);

    SongInfoDto? GetSongInfo(long? songId);

    ArtistInfoDto GetArtist(string artistHash);
    
    ArtistPageInfoDto GetArtistInfo(string artistHash);
}