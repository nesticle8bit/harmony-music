using Harmony.Music.Entities.Mappings;
using Harmony.Music.Entities.Music;
using Microsoft.EntityFrameworkCore;

namespace Harmony.Music.Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options) { }

    public DbSet<Song> Songs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SongsMap());

        base.OnModelCreating(modelBuilder);
    }
}