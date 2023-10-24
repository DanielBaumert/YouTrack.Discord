using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.YouTrack.Api;

public class Cell
{
    [JsonPropertyName("issuesCount")]
    public int IssuesCount { get; set; }
    
    [JsonPropertyName("issues")]
    public Issue[] Issues { get; set; }

    [JsonPropertyName("column")] 
    public Column Columns { get; set; }
}