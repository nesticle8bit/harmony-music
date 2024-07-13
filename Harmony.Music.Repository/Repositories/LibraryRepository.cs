using Harmony.Music.Contracts.Interfaces;
using Harmony.Music.Entities.Music;
using Harmony.Music.Repository.RepositoryBase;

namespace Harmony.Music.Repository.Repositories;

public class LibraryRepository : RepositoryBase<Library>, ILibraryRepository
{
    public LibraryRepository(RepositoryContext context) : base(context)
    {
    }

    public int SyncLibrary(List<string> songsList)
    {
        int songsImported = 0;

        foreach (var song in songsList)
        {
            CreateLibrary(new Library()
            {
                Path = song
            });

            songsImported++;
        }
        
        // TODO: Implement the library save with parallel forEach
        // Parallel.ForEach(songsList, song =>
        // {
        //     CreateAsync(new Library()
        //     {
        //         Path = song
        //     });
        //
        //     songsImported++;
        // });

        return songsImported;
    }

    public void CreateLibrary(Library library) => Create(library);

    public void UpdateLibrary(Library library) => Update(library);
}