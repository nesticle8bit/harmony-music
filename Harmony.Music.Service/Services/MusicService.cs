using AutoMapper;
using Harmony.Music.Contracts.Manager;
using Harmony.Music.Entities.Music;
using Harmony.Music.ServiceContracts.Services;
using Harmony.Music.Shared.DataTransferObjects;
using Harmony.Music.Shared.DataTransferObjects.Library;
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

        // TODO: Set path from user config
        List<string> allFiles = GetAllFiles("/mnt/Musica/Music/Black Metal/United States", librarySyncReport.Errors);

        librarySyncReport.SongsFound = allFiles.Count;
        librarySyncReport.SongsImported = _repository.LibraryRepository.SyncLibrary(allFiles);
        _repository.Save();

        return librarySyncReport;
    }

    public ExtractMusicMetadataReportDto ExtractMusicMetadata()
    {
        ExtractMusicMetadataReportDto report = new();
        var files = _repository.LibraryRepository.SearchLibraries(null, false)?
            .OrderBy(x => x.Path)
            // .Take(1000)
            .ToList();

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
                    var albumId = GetOrCreateAlbum(new Album
                    {
                        Artists = new List<long> { artistId.Value },
                        Title = metadata.TrackProperties.Album,
                        Disc = (int)metadata.TrackProperties.Disc,
                        Year = (int)metadata.TrackProperties.Year,
                        Type = AlbumTypesEnum.Pending,
                        Genres = metadata.TrackProperties.Genres?.ToList()
                    }, new Artist { Id = artistId.Value, Name = artist });

                    GetOrCreateSong(new CreateSongDto
                    {
                        FilePath = file.Path,
                        Artists = new List<long> { artistId.Value },
                        AlbumId = albumId,
                        Metadata = metadata,
                        Artist = new ArtistInfoDto { Name = artist },
                        Album = new AlbumInfoDto { Title = metadata.TrackProperties.Album ?? "Unknown" },
                        LibraryId = file.Id
                    });

                    SetProcessedLibraryRow(file.Id);
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

    public int ExtractAlbumsArtwork()
    {
        var albumsWithoutArtwork = _repository.AlbumRepository.SearchAlbums(new SearchAlbumDto() { HasArtwork = false }, false)
            .Take(1000)
            .ToList();

        if (albumsWithoutArtwork?.Count < 1)
            return 0;

        var processed = 0;

        foreach (var album in albumsWithoutArtwork)
        {
            // ExtractAlbumArt(metadata, "06225f58c6");
        }

        return processed;
    }

    public void ExtractAlbumArt(MediaMetadataDto metadata, string albumHash)
    {
        var directory = Path.GetDirectoryName(metadata.Name);
        string[] extensions = [".jpg", ".jpeg", ".png", ".bmp"];
        string[] fileNames = ["cover", "albumart", "folder"];

        string albumart = "";
        foreach (var file in Directory.EnumerateFiles(directory))
        {
            if (!string.IsNullOrEmpty(albumart))
                break;

            if (extensions.Contains(Path.GetExtension(file).ToLower()) && fileNames.Contains(Path.GetFileNameWithoutExtension(file).ToLower()))
            {
                albumart = file;
            }
        }

        if (string.IsNullOrEmpty(albumart))
            return;

        var imageFolder = Path.Combine("./", "wwwroot", "albums", albumHash);

        if (!Directory.Exists(imageFolder))
            Directory.CreateDirectory(imageFolder);

        File.Copy(albumart, Path.Combine(imageFolder, $"{albumHash}{Path.GetExtension(albumart)}"), true);
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
            .FirstOrDefault();

        var mapped = _mapper.Map<SongInfoDto>(song);
        return mapped;
    }

    public ArtistInfoDto GetArtist(string artistHash)
    {
        var artist = _repository.ArtistRepository.SearchArtist(new SearchArtistDto() { Hash = artistHash }, false).FirstOrDefault();
        var mapped = _mapper.Map<ArtistInfoDto>(artist);

        return mapped;
    }

    public ArtistPageInfoDto GetArtistInfo(string artistHash)
    {
        var artist = _repository.ArtistRepository.SearchArtist(new SearchArtistDto() { Hash = artistHash }, false)
            .FirstOrDefault();

        var mapped = _mapper.Map<ArtistPageInfoDto>(artist);
        mapped.Albums = mapped.Albums?
            .GroupBy(x => new AlbumInfoDto { Hash = x.Hash, Artwork = x.Artwork, Title = x.Title, Year = x.Year })
            .Select(x => x.Key)
            .OrderBy(x => x.Year)
            .ToList();

        return mapped;
    }

    public long? GetOrCreateArtist(string artist)
    {
        var entity = _repository.ArtistRepository
            .SearchArtist(new SearchArtistDto { Name = artist }, false)
            .FirstOrDefault();

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
        var entity = _repository.AlbumRepository
            .SearchAlbums(new SearchAlbumDto { Title = album.Title, ArtistId = artist?.Id }, true)
            .FirstOrDefault();

        if (entity == null)
        {
            entity = new Album()
            {
                Title = album.Title,
                Artists = album.Artists,
                Disc = album.Disc,
                Year = album.Year,
                Type = album.Type,
                Genres = album.Genres,
                Hash = HashHelper.CreateHash([artist.Name, album.Title])
            };

            _repository.AlbumRepository.CreateAlbum(entity);
        }
        else
        {
            if (entity.Artists == null)
                entity.Artists = new List<long>();

            entity.Artists.AddRange(entity.Artists);
            entity.Artists = entity.Artists.Distinct().ToList();

            _repository.AlbumRepository.UpdateAlbum(entity);
        }

        _repository.Save();
        return entity.Id;
    }

    public long GetOrCreateSong(CreateSongDto createSongDto)
    {
        string songName = !string.IsNullOrEmpty(createSongDto.Metadata?.TrackProperties?.Title) ? createSongDto.Metadata.TrackProperties.Title?.Trim() : "Unknown";
        string hashName = HashHelper.CreateHash([createSongDto.Artist.Name, createSongDto.Album?.Title, songName]);

        var entity = _repository.SongRepository.SearchSongs(new SearchSongDto
        {
            AlbumId = createSongDto.AlbumId,
            LibraryId = createSongDto.LibraryId,
            Hash = hashName
        }, true).FirstOrDefault();

        if (entity == null)
        {
            entity = new Song
            {
                AlbumId = createSongDto.AlbumId,
                LibraryId = createSongDto.LibraryId,
                Hash = hashName,
                Artists = createSongDto.Artists,
                Track = createSongDto.Metadata?.TrackProperties != null ? (int)createSongDto.Metadata.TrackProperties.Track : 0,
                Name = !string.IsNullOrEmpty(createSongDto.Metadata?.TrackProperties?.Title) ? createSongDto.Metadata.TrackProperties.Title?.Trim() : "Unknown",
                Description = createSongDto.Metadata?.Description,
                Mimetype = !string.IsNullOrEmpty(createSongDto.Metadata?.Mimetype) ? createSongDto.Metadata.Mimetype : "unknown",
                PossiblyCorrupt = createSongDto.Metadata?.PossiblyCorrupt ?? true,
                Lyrics = createSongDto.Metadata?.TrackProperties?.Lyrics,
                MediaProperties = createSongDto.Metadata?.MediaProperties,
            };

            _repository.SongRepository.CreateSong(entity);
        }
        else
        {
            if (entity.Artists == null)
                entity.Artists = new List<long>();

            entity.Artists.AddRange(createSongDto.Artists);
            entity.Artists = entity.Artists.Distinct().ToList();

            _repository.SongRepository.UpdateSong(entity);
        }

        _repository.Save();
        return entity.Id;
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
                Lyrics = tagLib.Tag.Lyrics,
                Title = tagLib.Tag.Title,
            }
        };

        return metadata;
    }

    public bool SetProcessedLibraryRow(string? libraryId)
    {
        var entity = _repository.LibraryRepository.SearchLibraries(new SearchLibraryDto { Id = libraryId }, true)
            .FirstOrDefault();

        if (entity == null)
            return false;

        entity.HasBeenProcessed = true;
        _repository.LibraryRepository.UpdateLibrary(entity);
        _repository.Save();

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