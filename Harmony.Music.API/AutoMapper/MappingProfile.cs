using AutoMapper;
using Harmony.Music.Entities.Music;
using Harmony.Music.Shared.DataTransferObjects.Music;

namespace Harmony.Music.API.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Artist, ArtistInfoDto>();
        CreateMap<Album, AlbumInfoDto>();
        CreateMap<Song, SongInfoDto>();
    }
}