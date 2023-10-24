﻿namespace YouTrack.Discord.Api.Discord.Api;

public enum ChannelType
{
    /// <summary>
    /// a text channel within a server
    /// </summary>
    GUILD_TEXT = 0,
    /// <summary>
    /// a direct message between users
    /// </summary>
    DM = 1,
    /// <summary>
    /// a voice channel within a server
    /// </summary>
    GUILD_VOICE = 2,
    /// <summary>
    /// a direct message between multiple users
    /// </summary>
    GROUP_DM = 3,
    /// <summary>
    /// an organizational category that contains up to 50 channels
    /// </summary>
    GUILD_CATEGORY = 4,
    /// <summary>
    /// a channel that users can follow and crosspost into their own server (formerly news channels)
    /// </summary>
    GUILD_ANNOUNCEMENT = 5,
    /// <summary>
    /// a temporary sub-channel within a GUILD_ANNOUNCEMENT channel
    /// </summary>
    ANNOUNCEMENT_THREAD = 10,
    /// <summary>
    /// a temporary sub-channel within a GUILD_TEXT or GUILD_FORUM channel
    /// </summary>
    PUBLIC_THREAD = 11,
    /// <summary>
    /// a temporary sub-channel within a GUILD_TEXT channel that is only viewable by those invited and those with the MANAGE_THREADS permission
    /// </summary>
    PRIVATE_THREAD = 12,
    /// <summary>
    /// a voice channel for hosting events with an audience
    /// </summary>
    GUILD_STAGE_VOICE = 13,
    /// <summary>
    /// the channel in a hub containing the listed servers
    /// </summary>
    GUILD_DIRECTORY = 14,
    /// <summary>
    /// Channel that can only contain threads
    /// </summary>
    GUILD_FORUM = 15,
}