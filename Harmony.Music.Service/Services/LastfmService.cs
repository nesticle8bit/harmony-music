using AutoMapper;
using Harmony.Music.ServiceContracts.Services;
using Harmony.Music.Shared.DataTransferObjects.Lastfm;

namespace Harmony.Music.Service.Services;

public class LastfmService : ILastfmService
{
    private string LASTFM_KEY = "";
    private readonly IMapper _mapper;
    private HttpClient _httpClient;

    public LastfmService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<List<LastfmTrackDto>?> GetTopTracks(string? artist)
    {
        _httpClient = new HttpClient();

        var url = $"http://ws.audioscrobbler.com/2.0/?method=artist.gettoptracks&artist={Uri.EscapeDataString(artist)}&api_key={LASTFM_KEY}&format=json";
        var response = await _httpClient.GetStringAsync(url);
        
        var topTracksResponse = System.Text.Json.JsonSerializer.Deserialize<LastfmTopTracksResponse>(response);

        return topTracksResponse?.TopTracks?.Track;
    }
}