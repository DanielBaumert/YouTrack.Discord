using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.Discord.Api;

public class Message
{
    /// <summary>
    /// id of the message
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    /// id of the channel the message was sent in
    /// </summary>
    [JsonPropertyName("channel_id")]
    public string ChannelID { get; set; }

    /// <summary>
    /// the author of this message
    /// </summary>
    [JsonPropertyName("author")]
    public User Author { get; set; }
    
    /// <summary>
    /// contents of the message
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; }
	
    /// <summary>
	/// any embedded content
	/// </summary>
    [JsonPropertyName("embeds")]
    public Embed[] Embeds { get; set; }
    
    /// <summary>
    /// type of message
    /// </summary>
    [JsonPropertyName("type")]
    public MessageTypes type { get; set; }
    
    /// <summary>
    /// type of message
    /// </summary>
    [JsonPropertyName("flags")]
    public MessageFlags Flags { get; set; }
    
    /// <summary>
    /// A generally increasing integer (there may be gaps or duplicates) that represents the approximate position of the message in a thread,
    /// it can be used to estimate the relative position of the message in a thread in company with total_message_sent on parent thread
    /// </summary>
    [JsonPropertyName("position")]
    public int? Position { get; set; }
}

public enum MessageTypes
{
	DEFAULT = 0,
	RECIPIENT_ADD = 1,
	RECIPIENT_REMOVE = 2,
	CALL = 3,
	CHANNEL_NAME_CHANGE = 4,
	CHANNEL_ICON_CHANGE = 5,
	CHANNEL_PINNED_MESSAGE = 6,
	USER_JOIN = 7,
	GUILD_BOOST = 8,
	GUILD_BOOST_TIER_1 = 9,
	GUILD_BOOST_TIER_2 = 10,
	GUILD_BOOST_TIER_3 = 11,
	CHANNEL_FOLLOW_ADD = 12,
	GUILD_DISCOVERY_DISQUALIFIED = 14,
	GUILD_DISCOVERY_REQUALIFIED = 15,
	GUILD_DISCOVERY_GRACE_PERIOD_INITIAL_WARNING = 16,
	GUILD_DISCOVERY_GRACE_PERIOD_FINAL_WARNING = 17,
	THREAD_CREATED = 18,
	REPLY = 19,
	CHAT_INPUT_COMMAND = 20,
	THREAD_STARTER_MESSAGE = 21,
	GUILD_INVITE_REMINDER = 22,
	CONTEXT_MENU_COMMAND = 23,
	AUTO_MODERATION_ACTION = 24
}