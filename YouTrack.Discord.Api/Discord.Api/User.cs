using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.Discord.Api;

public class User
{
    /// <summary>
    /// the user's id identify
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    /// <summary>
    /// the user's username, not unique across the platform
    /// </summary>
    [JsonPropertyName("username")]
    public string Username { get; set; }

    /// <summary>
    /// the user's 4-digit discord-tag
    /// </summary>
    [JsonPropertyName("discriminator")]
    public string Discriminator { get; set; }
    
    /// <summary>
    /// the user's avatar hash
    /// </summary>
    [JsonPropertyName("avatar")]
    public string Avatar { get; set; }

    /// <summary>
    /// whether the user belongs to an OAuth2 application	
    /// </summary>
    [JsonPropertyName("bot")]
    public bool? Bot { get; set; }

    /// <summary>
    /// whether the user is an Official Discord System user (part of the urgent message system)		
    /// </summary>
    [JsonPropertyName("system")]
    public bool? System { get; set; }
    
    /// <summary>
    /// whether the user has two factor enabled on their account	
    /// </summary>
    [JsonPropertyName("mfa_enabled")]
    public bool? MfaEnabled { get; set; }
    
    /// <summary>
    /// the user's banner hash
    /// </summary>
    [JsonPropertyName("banner")]
    public string? Banner { get; set; }
    
    /// <summary>
    /// the user's banner color encoded as an integer representation of hexadecimal color code
    /// </summary>
    [JsonPropertyName("accent_color")]
    public int? AccentColor { get; set; }
    
    /// <summary>
    /// the user's chosen language option
    /// </summary>
    [JsonPropertyName("locale")]
    public string? Locale { get; set; }
    
    /// <summary>
    /// whether the email on this account has been verified
    /// </summary>
    [JsonPropertyName("verified")]
    public bool? Verified { get; set; }
 
    /// <summary>
    /// the user's email
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    
    /// <summary>
    /// the flags on a user's account
    /// </summary>
    [JsonPropertyName("flags")]
    public UserFlags? Flags { get; set; }
    
    /// <summary>
    /// the type of Nitro subscription on a user's account
    /// </summary>
    [JsonPropertyName("premium_type")]
    public PremiumTypes? PremiumType { get; set; }

    /// <summary>
    /// the public flags on a user's account
    /// </summary>
    [JsonPropertyName("public_flags")]
    public UserFlags? PublicFlags { get; set; }
    
}