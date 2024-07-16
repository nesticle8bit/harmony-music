using System.Security.Cryptography;
using System.Text;

namespace Harmony.Music.Shared.Helpers;

public static class HashHelper
{
    public static string CreateHash(string[] args, bool decode = false, int limit = 10)
    {
        string RemoveNonAlphanumeric(string token)
        {
            token = token.ToLower().Trim().Replace(" ", "");
            var result = new StringBuilder();
            
            foreach (var c in token.Where(char.IsLetterOrDigit))
            {
                result.Append(c);
            }

            var cleanedToken = result.ToString();
            return string.IsNullOrEmpty(cleanedToken) ? token : cleanedToken;
        }

        // Combine the processed arguments into one string
        var combined = string.Join("", args.Select(RemoveNonAlphanumeric));

        // Decode the combined string if decode is true
        if (decode)
            combined = Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-1").GetBytes(combined));

        // Compute the SHA-1 hash
        using var sha1 = SHA1.Create();
        var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(combined));
        var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

        // Return the truncated hash
        return limit % 2 == 0
            ? hashString[..(limit / 2)] + hashString[^(limit / 2)..]
            : hashString[..(limit / 2)] + hashString[(hashString.Length - limit / 2 - 1)..];
    }
}