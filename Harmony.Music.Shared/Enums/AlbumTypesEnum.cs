using System.ComponentModel;

namespace Harmony.Music.Shared.Enums;

public enum AlbumTypesEnum
{
    [Description("Split")]
    Split,
    [Description("EP")]
    EP,
    [Description("Demo")]
    Demo,
    [Description("Full-length")]
    FullLength,
    [Description("Pending")]
    Pending
}