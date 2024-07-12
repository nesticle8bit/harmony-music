using Harmony.Music.Entities.Music;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Harmony.Music.Entities.Mappings;

public class SongsMap : IEntityTypeConfiguration<Song>
{
    void IEntityTypeConfiguration<Song>.Configure(EntityTypeBuilder<Song> builder)
    {
        builder.HasIndex(x => x.Id).IsUnique();
    }
}