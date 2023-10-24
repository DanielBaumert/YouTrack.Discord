using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.YouTrack.Api;

public class Issue
{
    [JsonPropertyName("id")]
    public string ID { get; set; }

    [JsonPropertyName("idReadable")]
    public string IDReadable { get; set; }
    
    [JsonPropertyName("summary")] 
    public string Summary { get; set; }
    
    [JsonPropertyName("description")] 
    public string Description { get; set; }

    [JsonPropertyName("comments")] 
    public IssueComment[] Comments { get; set; }
}