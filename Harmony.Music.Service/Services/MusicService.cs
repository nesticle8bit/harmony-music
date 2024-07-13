using AutoMapper;
using Harmony.Music.Contracts.Manager;
using Harmony.Music.Entities.Music;
using Harmony.Music.ServiceContracts.Services;
using Harmony.Music.Shared.DataTransferObjects;
using Harmony.Music.Shared.DataTransferObjects.Music;

namespace Harmony.Music.Service.Services;

public class MusicService : IMusicService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public MusicService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public MediaMetadataDto GetMediaMetadata()
    {
        var tagLib = TagLib.File.Create("");

        var metadata = new MediaMetadataDto()
        {
            Name = tagLib.Name,
            Description = tagLib.Properties.Description,
            Mimetype = tagLib.MimeType,
            PossiblyCorrupt = tagLib.PossiblyCorrupt,
            MediaProperties = new MediaPropertyDto()
            {
                Duration = tagLib.Properties.Duration,
                AudioBitrate = tagLib.Properties.AudioBitrate,
                AudioChannels = tagLib.Properties.AudioChannels,
                AudioSampleRate = tagLib.Properties.AudioSampleRate,
                BitsPerSample = tagLib.Properties.BitsPerSample,
                Length = 0 // TODO: Length of the file
            },
            TrackProperties = new TrackPropertyDto()
            {
                Artists = tagLib.Tag.Artists?.ToList(),
                Album = tagLib.Tag.Album,
                Disc = tagLib.Tag.Disc,
                DiscCount = tagLib.Tag.DiscCount,
                Genres = tagLib.Tag.Genres?.ToList(),
                Track = tagLib.Tag.Track,
                TrackCount = tagLib.Tag.TrackCount,
                Year = tagLib.Tag.Year,
                MusicBrainzDiscId = tagLib.Tag.MusicBrainzDiscId,
                Lyrics = tagLib.Tag.Lyrics,
                Title = tagLib.Tag.Title,
            }
        };

        return metadata;
    }

    public LibrarySyncReportDto SyncLibrary()
    {
        LibrarySyncReportDto librarySyncReport = new();
        List<string> allFiles = GetAllFiles("/mnt/Musica/Music", librarySyncReport.Errors);

        librarySyncReport.SongsFound = allFiles.Count;
        librarySyncReport.SongsImported = _repository.LibraryRepository.SyncLibrary(allFiles);
        _repository.Save();

        return librarySyncReport;
    }

    private List<string> GetAllFiles(string path, List<LibrarySyncErrorsDto> errors)
    {
        var musicFiles = new List<string>();
        var dirs = new Queue<string>();
        dirs.Enqueue(path);

        string[] musicExtensions = { ".mp3", ".flac", ".wav", ".aac", ".ogg", ".wma", ".m4a" };

        try
        {
            while (dirs.Count > 0)
            {
                string currentDir = dirs.Dequeue();
                try
                {
                    foreach (string file in Directory.EnumerateFiles(currentDir))
                    {
                        if (musicExtensions.Contains(Path.GetExtension(file).ToLower()))
                        {
                            musicFiles.Add(file);
                        }
                    }

                    foreach (string subDir in Directory.EnumerateDirectories(currentDir))
                    {
                        dirs.Enqueue(subDir);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., access denied)
                    Console.WriteLine(ex.Message);
                }
            }
        }
        catch (Exception e)
        {
            errors.Add(new LibrarySyncErrorsDto()
            {
                Error = e.Message,
                StackTrace = e.StackTrace
            });
        }

        return musicFiles;
    }
}