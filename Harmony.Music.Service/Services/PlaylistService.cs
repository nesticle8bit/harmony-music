using AutoMapper;
using Harmony.Music.Contracts.Manager;
using Harmony.Music.ServiceContracts.Services;
using Harmony.Music.Shared.DataTransferObjects.Library;
using Harmony.Music.Shared.DataTransferObjects.Music;
using Harmony.Music.Shared.DataTransferObjects.Playlists;
using Microsoft.EntityFrameworkCore;

namespace Harmony.Music.Service.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public PlaylistService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public List<PlaylistDto> GetRecentlyAdded()
    {
        var recentlyAdded = _repository.SongRepository.SearchSongs(null, false)?
            .OrderByDescending(x => x.Id)
            .Include(x => x.Album)
            .Take(12)
            .ToList();

        var playlists = new List<PlaylistDto>();

        if (recentlyAdded is null)
            return playlists;

        playlists = recentlyAdded.Select(x => new PlaylistDto()
        {
            Id = x.Id.ToString(),
            Name = x.Name,
            Image = $"{x.Album?.Hash}.jpg",
            // Artist = new ArtistInfoDto
            // {
            //     Hash = x.Artist?.Hash,
            //     Name = x.Artist?.Name
            // },
            DateCreated = x.DateCreated
        }).ToList();

        return playlists;
    }
}