using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.YouTrack.Api;

public class ColumnSettings
{
    [JsonPropertyName("columns")]
    public Column[] Columns { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }
}