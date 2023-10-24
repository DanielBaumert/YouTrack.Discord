using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.YouTrack.Api;

public class User
{
    [JsonPropertyName("id")]
    public string ID { get; set; }

    [JsonPropertyName("login")]
    public string Login { get; set; }
    
    [JsonPropertyName("fullName")]
    public string FullName { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
}