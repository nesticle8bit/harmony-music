using Harmony.Music.Entities.Music;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Harmony.Music.Entities.Mappings;

public class SongsMap : IEntityTypeConfiguration<Song>
{
    void IEntityTypeConfiguration<Song>.Configure(EntityTypeBuilder<Song> builder)
    {
        builder.ToTable("songs", "music");

        builder.HasIndex(x => x.Id).IsUnique();

        builder.HasOne(x => x.Album)
            .WithMany(x => x.Songs)
            .HasForeignKey(x => x.AlbumId);
    }
}

public class AlbumsMap : IEntityTypeConfiguration<Album>
{
    void IEntityTypeConfiguration<Album>.Configure(EntityTypeBuilder<Album> builder)
    {
        builder.ToTable("albums", "music");

        builder.HasIndex(x => x.Id).IsUnique();
    }
}

public class ArtistsMap : IEntityTypeConfiguration<Artist>
{
    void IEntityTypeConfiguration<Artist>.Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.ToTable("artists", "music");

        builder.HasIndex(x => x.Id).IsUnique();
    }
}

public class ArtistAlbumsMap : IEntityTypeConfiguration<ArtistAlbums>
{
    void IEntityTypeConfiguration<ArtistAlbums>.Configure(EntityTypeBuilder<ArtistAlbums> builder)
    {
        builder.ToTable("artistsAlbums", "music");

        builder.HasIndex(x => x.Id).IsUnique();

        builder.HasOne(x => x.Album)
            .WithMany(x => x.ArtistAlbums)
            .HasForeignKey(x => x.AlbumId);

        builder.HasOne(x => x.Artist)
            .WithMany(x => x.ArtistAlbums)
            .HasForeignKey(x => x.ArtistId);
    }
}

public class LibraryMap : IEntityTypeConfiguration<Library>
{
    void IEntityTypeConfiguration<Library>.Configure(EntityTypeBuilder<Library> builder)
    {
        builder.ToTable("library", "music");

        builder.HasIndex(x => x.Id).IsUnique();
    }
}