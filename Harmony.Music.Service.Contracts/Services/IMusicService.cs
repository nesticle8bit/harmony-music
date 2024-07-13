using Harmony.Music.Shared.DataTransferObjects;
using Harmony.Music.Shared.DataTransferObjects.Music;

namespace Harmony.Music.ServiceContracts.Services;

public interface IMusicService
{
    MediaMetadataDto GetMediaMetadata();

    LibrarySyncReportDto SyncLibrary();
}