using AutoMapper;
using Harmony.Music.Contracts.Manager;
using Harmony.Music.Entities.Music;
using Harmony.Music.ServiceContracts.Services;
using Harmony.Music.Shared.DataTransferObjects;
using Harmony.Music.Shared.DataTransferObjects.Music;
using Harmony.Music.Shared.Enums;

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

    public LibrarySyncReportDto SyncLibrary()
    {
        LibrarySyncReportDto librarySyncReport = new();
        List<string> allFiles = GetAllFiles("/mnt/Musica/Music", librarySyncReport.Errors);

        librarySyncReport.SongsFound = allFiles.Count;
        librarySyncReport.SongsImported = _repository.LibraryRepository.SyncLibrary(allFiles);
        _repository.Save();

        return librarySyncReport;
    }

    public ExtractMusicMetadataReportDto ExtractMusicMetadata()
    {
        ExtractMusicMetadataReportDto report = new();
        var files = _repository.LibraryRepository.SearchLibraries(null, false)?.Take(1).ToList();

        if (files?.Count < 1)
            return report;

        foreach (var file in files)
        {
            try
            {
                if (string.IsNullOrEmpty(file.Path))
                    continue;

                if (!File.Exists(file.Path))
                {
                    report.Errors.Add(new LibrarySyncErrorsDto()
                    {
                        Error = $"File does not exists and will be removed from the library",
                        StackTrace = file.Path
                    });
                    
                    _repository.LibraryRepository.DeleteLibrary(file.Id);
                    _repository.Save();
                    
                    report.SongsRemoved++;
                    continue;
                }

                var metadata = GetMediaMetadata(file.Path);
                report.SongsProcessed++;

                if (metadata?.TrackProperties == null)
                {
                    report.Errors.Add(new LibrarySyncErrorsDto()
                    {
                        Error = $"Trackproperties metadata has not been found",
                        StackTrace = file.Path
                    });
                    continue;
                }

                if (metadata.TrackProperties?.Artists == null)
                    metadata.TrackProperties.Artists = new List<string>() { "Unknown" };

                foreach (var artistValue in metadata.TrackProperties.Artists)
                {
                    var artistId = GetArtist(artistValue, true);

                    if (!artistId.HasValue)
                        continue;

                    var albumEntity = new Album()
                    {
                        Title = metadata.TrackProperties.Title,
                        Disc = (int)metadata.TrackProperties.Disc,
                        Year = (int)metadata.TrackProperties.Year,
                        Type = AlbumTypesEnum.Pending,
                        Genres = metadata.TrackProperties.Genres
                    };

                    var albumId = GetAlbum(albumEntity, artistId, true);

                    if (!albumId.HasValue)
                        continue;

                    var fileInfo = new FileInfo(file.Path);
                    metadata.MediaProperties.Length = fileInfo.Length;
                    
                    var song = new Song()
                    {
                        AlbumId = albumId.Value,
                        Track = (int)metadata.TrackProperties.Track,
                        Name = metadata.TrackProperties.Title?.Trim(),
                        Description = metadata.Description,
                        Mimetype = !string.IsNullOrEmpty(metadata.Mimetype) ? metadata.Mimetype : "unknown",
                        PossiblyCorrupt = metadata.PossiblyCorrupt,
                        Lyrics = metadata.TrackProperties.Lyrics,
                        MediaProperties = metadata.MediaProperties
                    };

                    var songCreated = _repository.SongRepository.SearchSongs(new SearchSongDto() { Name = song.Name, AlbumId = song.AlbumId }, false)?.FirstOrDefault();

                    if (songCreated != null)
                        throw new Exception($"The song '{song.Name}' has already been created. You can find it with the ID '{songCreated.Id}'.");
                    
                    _repository.SongRepository.CreateSong(song);
                    _repository.Save();
                }

                report.SongsImported++;
            }
            catch (Exception e)
            {
                report.Errors.Add(new LibrarySyncErrorsDto()
                {
                    StackTrace = e.StackTrace,
                    Error = e.Message
                });
            }
        }

        return report;
    }

    public long? GetArtist(string artist, bool createIfItDontExists = false)
    {
        var entity = _repository.ArtistRepository.SearchArtist(new SearchArtistDto() { Name = artist }, false)?.FirstOrDefault();

        if (entity == null)
        {
            if (!createIfItDontExists)
            {
                return null;
            }
            else
            {
                entity = new Artist()
                {
                    Name = artist
                };

                _repository.ArtistRepository.CreateArtist(entity);
                _repository.Save();

                return entity.Id;
            }
        }

        return entity.Id;
    }

    public long? GetAlbum(Album album, long? artistId, bool createIfItDontExists = false)
    {
        var entity = _repository.AlbumRepository.SearchAlbums(new SearchAlbumDto() { Title = album.Title }, false)?.FirstOrDefault();

        if (entity == null)
        {
            if (!createIfItDontExists)
            {
                return null;
            }

            entity = new Album()
            {
                
                Title = album.Title,
                Disc = album.Disc,
                Year = album.Year,
                Type = album.Type,
                Genres = album.Genres
            };

            _repository.AlbumRepository.CreateAlbum(entity);
            _repository.Save();

            var artistAlbums = _repository.ArtistAlbumsRepository.SearchArtistAlbums(new SearchArtistAlbumsDto() { AlbumId = entity.Id, ArtistId = artistId }, false)
                ?.FirstOrDefault();

            if (artistAlbums == null)
            {
                _repository.ArtistAlbumsRepository.CreateArtistAlbums(new ArtistAlbums() { AlbumId = entity.Id, ArtistId = artistId.Value });
                _repository.Save();
            }

            return entity.Id;
        }

        return entity.Id;
    }

    public MediaMetadataDto? GetMediaMetadata(string file)
    {
        if (string.IsNullOrEmpty(file))
            return null;

        var tagLib = TagLib.File.Create(file);

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