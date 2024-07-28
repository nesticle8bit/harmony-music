using Harmony.Music.Contracts.Interfaces;
using Harmony.Music.Entities.Music;
using Harmony.Music.Repository.RepositoryBase;
using Harmony.Music.Shared.DataTransferObjects.Library;

namespace Harmony.Music.Repository.Repositories;

public class LibraryRepository : RepositoryBase<Library>, ILibraryRepository
{
    public LibraryRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<Library> SearchLibraries(SearchLibraryDto? search, bool trackChanges)
    {
        var query = FindByCondition(x => !string.IsNullOrEmpty(x.Path) && !x.HasBeenProcessed, trackChanges);

        if (!string.IsNullOrEmpty(search?.Path))
            query = query.Where(x => x.Path.ToLower().Contains(search.Path.ToLower()));

        return query;
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

    public void DeleteLibrary(string id)
    {
        var library = SearchLibraries(new SearchLibraryDto() { Id = id }, true).FirstOrDefault();

        if (library is null)
            return;
        
        Delete(library);
    }
}