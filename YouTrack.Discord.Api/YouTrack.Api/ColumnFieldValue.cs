using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.YouTrack.Api;

public class ColumnFieldValue
{
    [JsonPropertyName("isResolved")]
    public bool IsResolved { get; set; }

    [JsonPropertyName("presentation")]
    public string Presentation { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("$type")]
    public string Type { get; set; }
}