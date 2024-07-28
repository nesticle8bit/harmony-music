using System.Net;
using Harmony.Music.Presentation.Helper;
using Harmony.Music.ServiceContracts.Manager;
using Microsoft.AspNetCore.Mvc;

namespace Harmony.Music.Presentation.Controllers;

[Route("api/sync")]
[ApiController]
public class SyncController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public SyncController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet("library")]
    public IActionResult SyncLibrary()
    {
        JsonObjectResult<dynamic> result = new()
        {
            Status = (int)HttpStatusCode.OK
        };

        try
        {
            result.Data = _serviceManager.MusicService.SyncLibrary();
        }
        catch (Exception e)
        {
            result.Errors?.Add(e.Message);
            result.Status = (int)HttpStatusCode.InternalServerError;

            return StatusCode(result.Status, result);
        }

        return Ok(result);
    }
    
    [HttpGet("metadata")]
    public IActionResult ExtractMusicMetadata()
    {
        JsonObjectResult<dynamic> result = new()
        {
            Status = (int)HttpStatusCode.OK
        };

        try
        {
            result.Data = _serviceManager.MusicService.ExtractMusicMetadata();
        }
        catch (Exception e)
        {
            result.Errors?.Add(e.Message);
            result.Status = (int)HttpStatusCode.InternalServerError;

            return StatusCode(result.Status, result);
        }

        return Ok(result);
    }
    
    [HttpGet("artwork")]
    public IActionResult ExtractAlbumsArtwork()
    {
        JsonObjectResult<dynamic> result = new()
        {
            Status = (int)HttpStatusCode.OK
        };

        try
        {
            result.Data = _serviceManager.MusicService.ExtractAlbumsArtwork();
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