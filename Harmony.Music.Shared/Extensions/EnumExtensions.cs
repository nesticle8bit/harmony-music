using System.ComponentModel;
using System.Reflection;

namespace Harmony.Music.Shared.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo? fi = value.GetType().GetField(value.ToString());
        var attributes = (DescriptionAttribute[]?)fi?.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Any() ? attributes.First().Description : value.ToString();
    }
}