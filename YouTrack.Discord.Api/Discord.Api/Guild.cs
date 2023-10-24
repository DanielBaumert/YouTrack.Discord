using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.Discord.Api;

public class Guild
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("permissions")]
    public string Permissions { get; set; }
}