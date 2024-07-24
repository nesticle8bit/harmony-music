using AutoMapper;
using Harmony.Music.Contracts.Manager;
using Harmony.Music.Entities.Music;
using Harmony.Music.ServiceContracts.Services;
using Harmony.Music.Shared.DataTransferObjects;
using Harmony.Music.Shared.DataTransferObjects.Music;
using Harmony.Music.Shared.Enums;
using Harmony.Music.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

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

    public ExtractMusicMetadataReportDto ExtractMusicMetadatav2()
    {
        ExtractMusicMetadataReportDto report = new();
        var files = _repository.LibraryRepository.SearchLibraries(null, false)?.Take(1000).ToList();

        if (files?.Count < 1)
            return report;

        foreach (var file in files)
        {
            try
            {
                if (!File.Exists(file.Path))
                {
                    MusicItemDoesNotExists(file, report);
                    continue;
                }

                report.SongsProcessed++;
                var metadata = GetMediaMetadata(file.Path);

                foreach (var artist in metadata.TrackProperties.Artists)
                {
                    var artistId = GetOrCreateArtist(artist);
                    var albumId = GetOrCreateAlbum(new Album()
                    {
                        Title = metadata.TrackProperties.Album,
                        Disc = (int)metadata.TrackProperties.Disc,
                        Year = (int)metadata.TrackProperties.Year,
                        Type = AlbumTypesEnum.Pending,
                        Genres = metadata.TrackProperties.Genres?.ToList()
                    }, new Artist() { Id = artistId.Value, Name = artist});
                    
                    var songId = GetOrCreateSong(file.Path, albumId, artistId, metadata);
                    SetProcessedLibraryRow(file, albumId, artistId);
                }

                report.SongsImported++;
            }
            catch (Exception e)
            {
                report.Errors.Add(new LibrarySyncErrorsDto()
                {
                    StackTrace = e.StackTrace,
                    Error = $"{e.Message} {e.InnerException?.Message}"
                });
            }
        }

        return report;
    }

    public string? GetFilePathBySongId(long? songId)
    {
        var song = _repository.SongRepository.SearchSongs(new SearchSongDto() { Id = songId }, false)?.FirstOrDefault();

        if (song == null)
            return string.Empty;

        return song?.MediaProperties?.Path;
    }

    public SongInfoDto? GetSongInfo(long? songId)
    {
        var song = _repository.SongRepository.SearchSongs(new SearchSongDto() { Id = songId }, false)?
            .Include(x => x.Album)
            .Include(x => x.Artist)
            .FirstOrDefault();

        var mapped = _mapper.Map<SongInfoDto>(song); 
        return mapped;
    }
    
    public long? GetOrCreateArtist(string artist)
    {
        var entity = _repository.ArtistRepository.SearchArtist(new SearchArtistDto() { Name = artist }, false)?.FirstOrDefault();

        if (entity == null)
        {
            entity = new Artist()
            {
                Name = artist,
                Hash = HashHelper.CreateHash([artist])
            };

            _repository.ArtistRepository.CreateArtist(entity);
            _repository.Save();
        }

        return entity.Id;
    }

    public long? GetOrCreateAlbum(Album album, Artist? artist)
    {
        var entity = _repository.AlbumRepository.SearchAlbums(new SearchAlbumDto() { Title = album.Title, ArtistId = artist.Id }, false)?.FirstOrDefault();

        if (entity == null)
        {
            entity = new Album()
            {
                Title = album.Title,
                Disc = album.Disc,
                Year = album.Year,
                Type = album.Type,
                Genres = album.Genres,
                Songs = null,
                Hash = HashHelper.CreateHash([artist.Name, album.Title])
            };

            _repository.AlbumRepository.CreateAlbum(entity);
            _repository.Save();
        }

        return entity.Id;
    }

    public long GetOrCreateSong(string filePath, long? albumId, long? artistId, MediaMetadataDto metadata)
    {
        var song = new Song()
        {
            AlbumId = albumId.Value,
            ArtistId = artistId.Value,
            Track = (int)metadata.TrackProperties.Track,
            Name = !string.IsNullOrEmpty(metadata.TrackProperties.Title) ? metadata.TrackProperties.Title?.Trim() : "Unknown",
            Description = metadata.Description,
            Mimetype = !string.IsNullOrEmpty(metadata.Mimetype) ? metadata.Mimetype : "unknown",
            PossiblyCorrupt = metadata.PossiblyCorrupt,
            Lyrics = metadata.TrackProperties.Lyrics,
            MediaProperties = metadata.MediaProperties,
        };

        var songCreated = _repository.SongRepository
            .SearchSongs(new SearchSongDto()
            {
                Name = song.Name,
                AlbumId = song.AlbumId,
                Path = filePath
            }, false)?.FirstOrDefault();

        if (songCreated != null)
            throw new Exception(
                $"The song '{song.Name}' ({metadata.MediaProperties?.Path}) has already been created. You can find it with the ID '{songCreated.Id}'.");

        _repository.SongRepository.CreateSong(song);
        _repository.Save();

        return song.Id;
    }

    public MediaMetadataDto? GetMediaMetadata(string file)
    {
        if (string.IsNullOrEmpty(file))
            return null;

        var tagLib = TagLib.File.Create(file);

        var fileInfo = new FileInfo(file);
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
                Path = file,
                Length = fileInfo.Length
            },
            TrackProperties = new TrackPropertyDto()
            {
                Artists = tagLib.Tag.Artists?.Length > 0 ? tagLib.Tag.Artists?.ToList() : new List<string>() { "Unknown" },
                Album = tagLib.Tag.Album,
                Disc = tagLib.Tag.Disc,
                DiscCount = tagLib.Tag.DiscCount,
                Genres = tagLib.Tag.Genres != null ? tagLib.Tag.Genres?.ToList() : new List<string>(),
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

    public bool SetProcessedLibraryRow(Library? library, long? albumId, long? artistId)
    {
        if (library is null)
            return false;

        library.HasBeenProcessed = true;
        library.AlbumId = albumId;
        library.ArtistId = artistId;
        _repository.LibraryRepository.UpdateLibrary(library);

        return true;
    }

    private void MusicItemDoesNotExists(Library? library, ExtractMusicMetadataReportDto report)
    {
        report.Errors.Add(new LibrarySyncErrorsDto()
        {
            Error = $"File does not exists and will be removed from the library",
            StackTrace = library.Path
        });

        _repository.LibraryRepository.DeleteLibrary(library.Id);
        _repository.Save();

        report.SongsRemoved++;
    }

    private List<string> GetAllFiles(string path, List<LibrarySyncErrorsDto> errors)
    {
        var musicFiles = new List<string>();
        var dirs = new Queue<string>();
        dirs.Enqueue(path);

        string[] musicExtensions = { ".mp3", ".flac", ".wav", ".aac", ".ogg", ".wma", ".m4a", ".opus" };

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

                    foreach (var subDir in Directory.EnumerateDirectories(currentDir))
                    {
                        dirs.Enqueue(subDir);
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