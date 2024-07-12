using System.Text.Json;
using System.Text.Json.Serialization;

namespace Harmony.Music.Presentation.Helper;

public class JsonObjectResult<T>
{
    [JsonPropertyName("errors")]
    public List<string>? Errors { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("data")]
    public T? Data { get; set; }

    public JsonObjectResult()
    {
        Status = 200;
        Errors = new List<string>();
    }

    public JsonObjectResult(int status)
    {
        Status = status;
        Errors = new List<string>();
    }

    public override string ToString() => JsonSerializer.Serialize(this);
}