using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.Discord.Api;

public class MessageCreateInfo
{
    [JsonPropertyName("content")]
    public string? Content { get; set; }
    
    [JsonPropertyName("message_reference")]
    public string? MessageReference { get; set; }
    
    [JsonPropertyName("embeds")]
    public Embed[]? Embeds { get; set; }
    
    [JsonPropertyName("flags")]
    public MessageFlags? Flags { get; set; }

    [JsonPropertyName("tts")]
    public bool TTS { get; set; }
}