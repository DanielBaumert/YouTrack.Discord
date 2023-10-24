using YouTrack.Discord.Api.Discord.Api;
using YouTrack.Discord.Api.YouTrack.Api;

namespace YouTrack.Discord.Api;

public static class Utils
{
    public static Dictionary<string, YouTrackChannelInfo> MapChannel(Column[] columns)
    {

        return columns.OrderBy(x => x.Ordinal)
            .ToDictionary(x => 
            x.ID, 
            y =>
            {
                var agileColumnFieldValue = y.FieldValues.First(x => x.Type.Equals("AgileColumnFieldValue"));

                return new YouTrackChannelInfo
                {
                    ID = y.ID,
                    Title = agileColumnFieldValue.Presentation,
                    Ordinal = y.Ordinal
                };
            });
    }

    public static async Task<IssueAvailableStatus> IsNewIssueAvailable(YouTrackRestClient client, Dictionary<string, Issue> previewIssues, CancellationToken cancellationToken = default)
    {
        Dictionary<string, Issue> issues = (await client.GetIssuesAsync(cancellationToken)).ToDictionary(x => x.ID, y => y);
    
        IssueAvailableStatus issueAvailableStatus = new IssueAvailableStatus
        {
            NewIssueAvailable = (previewIssues.Count < issues.Count)
        };
        
        if (issueAvailableStatus.NewIssueAvailable)
        {
            foreach (string key in previewIssues.Keys)
            {
                if (issues.ContainsKey(key))
                {
                    issues.Remove(key);
                }
            }

            issueAvailableStatus.NewIssues = issues;
        }
    
        return issueAvailableStatus;
    }
    
    
    public static async Task<ChannelInfo?> CreateNewIssueChannel(
        YouTrackRestClient youTrackClient,
        DiscordRestClient discord, 
        Guild guild, 
        ChannelInfo[]? subChannel,
        ChannelInfo youTrachCategory,
        Issue newIssue, 
        CancellationToken cancellationToken = default)
    {
        if (subChannel?.Any(x => x.Name.Equals(newIssue.IDReadable.ToLower())) ?? false)
        {
            return null;
        }
        
        ChannelInfo issueChannel = await discord.CreateChannelAsync(
            guild,
            new ChannelCreateInfo
            {
                Name = newIssue.IDReadable,
                Position = int.Parse(newIssue.ID.Split("-").Last()),
                ParentID = youTrachCategory.Id,
                Type = ChannelType.GUILD_TEXT
            },
            cancellationToken) ?? throw new Exception("Channel can't be creat.");
    
        Embed embedInfo = new Embed
        {
    
            Type = EmbedType.Rich.ToString().ToLower(),
            Color = 0xff0000,
            Title = newIssue.Summary,
            Description = !string.IsNullOrEmpty(newIssue.Description) ? ($"```md\n{newIssue.Description}\n```") : null,
            Url = $"{youTrackClient.Domain}issue/{newIssue.IDReadable}",
        };
    
        MessageCreateInfo messageCreateInfo = new MessageCreateInfo
        {
            Content = null,
            Embeds = new[]
            {
                embedInfo
            },
            Flags = null,
            MessageReference = null,
            TTS = false,
        };
    
        await discord.SendChannelMessageAsync(issueChannel, messageCreateInfo, cancellationToken);
        
        return issueChannel;
    }

    public static async Task SendTaskMove(
        DiscordRestClient discord, 
        ChannelInfo targetChannel,
        Issue issue, 
        string column,
        CancellationToken cancellationToken)
    {
        Embed embedInfo = new Embed
        {
        
            Type = EmbedType.Rich.ToString().ToLower(),
            Color = 0xff0000,
            Title = "State changed",
            Description = $"``{issue.Summary}`` is now in State ``{column}``",
            Url = null,
        };
        
        MessageCreateInfo messageCreateInfo = new MessageCreateInfo
        {
            Content = null,
            Embeds = new[]
            {
                embedInfo
            },
            Flags = null,
            MessageReference = null,
            TTS = false,
        };
        
        await discord.SendChannelMessageAsync(targetChannel, messageCreateInfo, cancellationToken);
    }
}


public struct YouTrackChannelInfo
{
    public string ID;
    public string Title;
    public int Ordinal;
}

public struct IssueAvailableStatus
{
    public bool NewIssueAvailable;
    public Dictionary<string, Issue> NewIssues;
}

public struct ManagedIssue
{
    public string ID;
    public string Summary;
    public string Description;
    public string CurrentColumn;
}
