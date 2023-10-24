using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.Discord.Api;

public class MessageRequestInfo
{
    [JsonPropertyName("around")]
    public string? Around { get; set; } = null;
    
    [JsonPropertyName("before")]
    public string? Before { get; set; } = null;
    
    [JsonPropertyName("after")]
    public string? After { get; set; } = null;

    [JsonPropertyName("limit")]
    public int? Limit { get; set; } = 50;
}