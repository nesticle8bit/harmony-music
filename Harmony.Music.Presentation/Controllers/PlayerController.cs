using System.Net;
using Harmony.Music.Presentation.Helper;
using Harmony.Music.ServiceContracts.Manager;
using Microsoft.AspNetCore.Mvc;

namespace Harmony.Music.Presentation.Controllers;

[Route("api/player")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public PlayerController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet("{songId}/play")]
    public IActionResult PlaySong(string songId)
    {
        JsonObjectResult<dynamic> result = new()
        {
            Status = (int)HttpStatusCode.OK
        };

        try
        {
            result.Data = songId;
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