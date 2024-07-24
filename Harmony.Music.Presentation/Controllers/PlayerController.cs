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
    
    [HttpGet("{songId}/info")]
    public IActionResult SongInfo(long? songId)
    {
        var songInfo = _serviceManager.MusicService.GetSongInfo(songId);
        return Ok(songInfo);
    }

    [HttpGet("{songId}/play")]
    public IActionResult PlaySong(long? songId)
    {
        var songFilePath = _serviceManager.MusicService.GetFilePathBySongId(songId);
        
        if (string.IsNullOrEmpty(songFilePath))
            return NotFound();

        return GetAudioFileStreamResult(songFilePath);
    }
    
    private FileStreamResult GetAudioFileStreamResult(string filePath)
    {
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var contentType = "audio/mpeg"; // TODO: get the contentType of the current file

        return new FileStreamResult(fileStream, contentType);
    }
}