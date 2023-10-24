using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.Discord.Api;

public class ChannelInfo
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("type")]
    public ChannelType Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("position")]
    public int Position { get; set; }

    [JsonPropertyName("flags")]
    public int Flags { get; set; }

    [JsonPropertyName("parent_id")]
    public string? ParentId { get; set; }

    [JsonPropertyName("guild_id")]
    public string GuildId { get; set; }
    
    [JsonPropertyName("last_message_id")]
    public string LastMessageId { get; set; }

    [JsonPropertyName("topic")]
    public object Topic { get; set; }
}