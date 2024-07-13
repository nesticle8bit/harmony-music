using Harmony.Music.Entities.Music;
using Harmony.Music.Shared.DataTransferObjects.Library;

namespace Harmony.Music.Contracts.Interfaces;

public interface ILibraryRepository
{
    IQueryable<Library> SearchLibraries(SearchLibraryDto? search, bool trackChanges);
    
    int SyncLibrary(List<string> songsList);
    
    void CreateLibrary(Library library);
    
    void UpdateLibrary(Library library);

    void DeleteLibrary(string id);
}