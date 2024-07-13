using Harmony.Music.Entities.Music;

namespace Harmony.Music.Contracts.Interfaces;

public interface ILibraryRepository
{
    int SyncLibrary(List<string> songsList);
    
    void CreateLibrary(Library library);
    
    void UpdateLibrary(Library library);
}