using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.YouTrack.Api;

public class ColumnsInfo
{
    [JsonPropertyName("columnSettings")]
    public ColumnSettings ColumnSettings { get; set; }
}