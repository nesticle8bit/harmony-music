using Harmony.Music.Entities.Music;

namespace Harmony.Music.Contracts.Interfaces;

public interface ILibraryRepository
{
    void CreateLibrary(Library library);
    
    void UpdateLibrary(Library library);
}