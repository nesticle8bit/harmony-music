using Harmony.Music.Entities.Mappings;
using Harmony.Music.Entities.Music;
using Microsoft.EntityFrameworkCore;

namespace Harmony.Music.Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options) { }

    public DbSet<Album> Album { get; set; }
    public DbSet<Artist> Artist { get; set; }
    public DbSet<Library> Library { get; set; }
    public DbSet<Song> Song { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SongsMap());
        modelBuilder.ApplyConfiguration(new AlbumsMap());
        modelBuilder.ApplyConfiguration(new ArtistsMap());
        modelBuilder.ApplyConfiguration(new LibraryMap());

        base.OnModelCreating(modelBuilder);
    }
}