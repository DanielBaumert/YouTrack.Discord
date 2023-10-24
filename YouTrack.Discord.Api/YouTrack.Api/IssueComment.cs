using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.YouTrack.Api;

public class IssueComment
{
    [JsonPropertyName("id")]
    public string ID { get; set; }
    
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("author")] 
    public User Author { get; set; }
    
}