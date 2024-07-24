using Harmony.Music.Shared.DataTransferObjects.Music;

namespace Harmony.Music.ServiceContracts.Services;

public interface IMusicService
{
    LibrarySyncReportDto SyncLibrary();

    ExtractMusicMetadataReportDto ExtractMusicMetadatav2();

    string GetFilePathBySongId(long? songId);

    SongInfoDto? GetSongInfo(long? songId);
}