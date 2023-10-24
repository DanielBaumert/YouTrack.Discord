using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.YouTrack.Api;

public class Column
{
    [JsonPropertyName("id")]
    public string ID { get; set; }
    
    [JsonPropertyName("fieldValues")]
    public ColumnFieldValue[] FieldValues { get; set; }

    [JsonPropertyName("ordinal")]
    public int Ordinal { get; set; }

    [JsonPropertyName("$type")]
    public string Type { get; set; }
}