using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.YouTrack.Api;

public class Project
{
    [JsonPropertyName("id")]
    public string ID { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}