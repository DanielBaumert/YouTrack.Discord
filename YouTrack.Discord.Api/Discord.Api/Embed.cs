using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.Discord.Api;

public class Embed
{
    [JsonPropertyName("title")] 
    public string? Title { get; set; }
    
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("color")]
    public int? Color { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}