using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.YouTrack.Api;

public class IssueTimeTracking
{
    [JsonPropertyName("workItems")]
    public List<WorkItem> WorkItems { get; set; }
}