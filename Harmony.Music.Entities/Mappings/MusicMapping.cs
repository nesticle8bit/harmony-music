using System.Text.Json;
using Harmony.Music.Entities.Music;
using Harmony.Music.Shared.DataTransferObjects;
using Harmony.Music.Shared.DataTransferObjects.Music;
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

        builder.Property(x => x.MediaProperties)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<MediaPropertyDto>(v, (JsonSerializerOptions?)null)
            );
    }
}

public class AlbumsMap : IEntityTypeConfiguration<Album>
{
    void IEntityTypeConfiguration<Album>.Configure(EntityTypeBuilder<Album> builder)
    {
        builder.ToTable("albums", "music");

        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Genres)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null)
            );
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

public class LibraryMap : IEntityTypeConfiguration<Library>
{
    void IEntityTypeConfiguration<Library>.Configure(EntityTypeBuilder<Library> builder)
    {
        builder.ToTable("library", "music");

        builder.HasIndex(x => x.Id).IsUnique();
    }
}