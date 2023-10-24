using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.YouTrack.Api;

public class IssueCreateInfo
{
    [JsonPropertyName("summary")]
    public string Summary { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("project")] 
    public ProjectInfo Project { get; set; }
}

public class ProjectInfo
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}