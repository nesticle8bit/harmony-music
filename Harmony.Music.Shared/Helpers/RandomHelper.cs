using System.Text;

namespace Harmony.Music.Shared.Helpers;

public static class RandomHelper
{
    public static string RandomString(int length) => GenerateRandomString("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length);

    static string GenerateRandomString(string dictionary, int length)
    {
        length = length > 0 ? length : 128;

        Random random = new();
        StringBuilder sb = new(length);

        for (var i = 0; i < length; i++)
        {
            var index = random.Next(dictionary.Length);
            sb.Append(dictionary[index]);
        }

        return sb.ToString();
    }
}