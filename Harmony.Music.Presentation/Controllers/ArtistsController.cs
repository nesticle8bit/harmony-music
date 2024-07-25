using System.Net;
using Harmony.Music.Presentation.Helper;
using Harmony.Music.ServiceContracts.Manager;
using Harmony.Music.Shared.DataTransferObjects.Music;
using Microsoft.AspNetCore.Mvc;

namespace Harmony.Music.Presentation.Controllers;

[Route("api/artists")]
[ApiController]
public class ArtistsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ArtistsController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet("{artistHash}/info")]
    public IActionResult GetArtistInfo(string artistHash)
    {
        JsonObjectResult<ArtistPageInfoDto> result = new()
        {
            Status = (int)HttpStatusCode.OK
        };

        try
        {
            result.Data = _serviceManager.MusicService.GetArtistInfo(artistHash);
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