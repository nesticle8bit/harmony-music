using Harmony.Music.Contracts.Interfaces;
using Harmony.Music.Entities.Music;
using Harmony.Music.Repository.RepositoryBase;

namespace Harmony.Music.Repository.Repositories;

public class SongRepository : RepositoryBase<Song>, ISongRepository
{
    public SongRepository(RepositoryContext context) : base(context)  { }

    public Song? GetSongByUniqueId(string uniqueId, bool trackChanges)
    {
        // TODO: fix
        return FindByCondition(x => x.Lyrics == uniqueId, trackChanges)
            .FirstOrDefault();
    }
}