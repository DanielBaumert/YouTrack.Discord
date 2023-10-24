namespace YouTrack.Discord.Api.Discord.Api;

public enum EmbedType
{
    /// <summary>
    /// generic embed rendered from embed attributes
    /// </summary>
    Rich, 
    /// <summary>
    /// image embed
    /// </summary>
    Image, 
    /// <summary>
    /// video embed
    /// </summary>
    Video, 
    /// <summary>
    /// animated gif image embed rendered as a video embed
    /// </summary>
    Gifv, 
    /// <summary>
    /// article embed
    /// </summary>
    Article, 
    /// <summary>
    /// link embed
    /// </summary> 
    Link
}