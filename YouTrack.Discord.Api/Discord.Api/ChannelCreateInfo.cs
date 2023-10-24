using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.Discord.Api;

public class ChannelCreateInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("type")]
    public ChannelType Type { get; set; }

    [JsonPropertyName("parent_id")]
    public string ParentID { get; set; }

    [JsonPropertyName("position")]
    public int Position { get; set; }
}