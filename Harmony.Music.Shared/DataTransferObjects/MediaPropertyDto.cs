namespace Harmony.Music.Shared.DataTransferObjects;

public class MediaPropertyDto
{
    public string? Path { get; set; }
    public long Length { get; set; }
    public int AudioBitrate { get; set; }
    public int AudioChannels { get; set; }
    public int AudioSampleRate { get; set; }
    public int BitsPerSample { get; set; }
    public TimeSpan Duration { get; set; }
}