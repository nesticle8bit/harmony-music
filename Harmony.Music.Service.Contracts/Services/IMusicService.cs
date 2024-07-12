using Harmony.Music.Shared.DataTransferObjects;

namespace Harmony.Music.ServiceContracts.Services;

public interface IMusicService
{
    MediaMetadataDto GetMediaMetadata();
}