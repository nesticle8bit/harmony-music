using System.Net;
using Harmony.Music.Presentation.Helper;
using Harmony.Music.ServiceContracts.Manager;
using Harmony.Music.Shared.DataTransferObjects.Lastfm;
using Harmony.Music.Shared.DataTransferObjects.Music;
using Microsoft.AspNetCore.Mvc;

namespace Harmony.Music.Presentation.Controllers;

[Route("api/songs")]
[ApiController]
public class SongsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public SongsController(IServiceManager serviceManager) => _serviceManager = serviceManager;
    
    [HttpGet("{artistHash}/top")]
    public async Task<IActionResult> GetArtistTopSongs(string artistHash)
    {
        JsonObjectResult<List<LastfmTrackDto>?> result = new()
        {
            Status = (int)HttpStatusCode.OK
        };

        try
        {
            var artist = _serviceManager.MusicService.GetArtist(artistHash);
            result.Data = await _serviceManager.LastfmService.GetTopTracks(artist?.Name);
        }
        catch (Exception e)
        {
            result.Errors?.Add(e.Message);
            result.Status = (int)HttpStatusCode.InternalServerError;

            return StatusCode(result.Status, result);
        }

        return Ok(result);
    }
}