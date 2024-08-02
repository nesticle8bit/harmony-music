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
            .ToList()
            .SelectMany(song => song.Artists.Select(artistId => new { Song = song, ArtistId = artistId }))
            .Join(_repository.ArtistRepository.SearchArtist(null, false),
                songArtist => songArtist.ArtistId,
                artist => artist.Id,
                (songArtist, artist) => new
                {
                    songArtist.Song,
                    Artist = artist
                })
            .ToList();

        var playlists = new List<PlaylistDto>();

        if (recentlyAdded is null)
            return playlists;
        
        playlists = recentlyAdded.Select(x => new PlaylistDto
        {
            Id = x.Song.Id.ToString(),
            Name = x.Song.Name,
            Image = $"{x.Song.Album?.Hash}.jpg",
            Artist = new ArtistInfoDto
            {
                Name = x.Artist.Name,
                Hash = x.Artist.Hash,
            },
            DateCreated = x.Song.DateCreated
        }).ToList();

        return playlists;
    }
}