using System.Net;
using Harmony.Music.Presentation.Helper;
using Harmony.Music.ServiceContracts.Manager;
using Microsoft.AspNetCore.Mvc;

namespace Harmony.Music.Presentation.Controllers;

[Route("api/playlists")]
[ApiController]
public class PlaylistsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public PlaylistsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    
    [HttpGet("recently-added")]
    public IActionResult RecentlyAddedPlaylist()
    {
        JsonObjectResult<dynamic> result = new()
        {
            Status = (int)HttpStatusCode.OK
        };

        try
        {
            result.Data = _serviceManager.PlaylistService.GetRecentlyAdded();
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