using Harmony.Music.Contracts.Interfaces;
using Harmony.Music.Entities.Music;
using Harmony.Music.Repository.RepositoryBase;

namespace Harmony.Music.Repository.Repositories;

public class LibraryRepository : RepositoryBase<Library>, ILibraryRepository
{
    public LibraryRepository(RepositoryContext context) : base(context)
    {
    }

    public void CreateLibrary(Library library) => Create(library);

    public void UpdateLibrary(Library library) => Update(library);
}