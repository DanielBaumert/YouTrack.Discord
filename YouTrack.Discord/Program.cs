using YouTrack.Discord;
using YouTrack.Discord.Api;
using YouTrack.Discord.Api.Discord.Api;
using YouTrack.Discord.Api.YouTrack.Api;

// Discord token
const string DISCORD_BOT_TOKEN = "[DISCORD-BOT-TOKEN]"; 
// Discord server name
const string DISCORD_GUILD_NAME = "SwP WI/SO"; 

const string YOUTRACK_BOT_BEARER_TOKEN = "[TOKEN]";
const string YOUTRACK_SERVER = "http://sstlab.informatik.tu-cottbus.de:8090/";
const string YOUTRACK_PROJECT_NAME = "Softwarepraktikum_GruppeC Kanban-Board";

CancellationTokenSource cts = new CancellationTokenSource();

// Init Discord
DiscordRestClient discord = DiscordRestClient.Create(DISCORD_BOT_TOKEN);
var guilds = await discord.GetGuildsAsync(cts.Token);

Guild swp = guilds?.FirstOrDefault(x => x.Name.Equals(DISCORD_GUILD_NAME)) ?? throw new Exception();

var channels = await discord.GetChannelAsync(swp, cts.Token);
var categorys = channels!.Where(x => x.Type == ChannelType.GUILD_CATEGORY);
var youTrack = categorys.First(x => x.Name.Equals("YouTrack"));

Console.WriteLine($"YouTrack-Catecory:");
Console.WriteLine($"\tID  : {youTrack.Id}");
Console.WriteLine($"\tName: {youTrack.Name}");

// init youtrack
YouTrackRestClient youTrackClient = YouTrackRestClient.Create(YOUTRACK_SERVER, YOUTRACK_BOT_BEARER_TOKEN);
var projects = await youTrackClient.GetProjectsAsync();
var project = projects?.FirstOrDefault(x => x.Name.Equals(YOUTRACK_PROJECT_NAME));
if (project == null)
{
    throw new Exception();
}

ChannelInfo[]? subChannel = (await discord.GetChannelAsync(swp, cts.Token))?
    .Where(x => x.ParentId?.Equals(youTrack.Id) ?? false)
    ?.ToArray();

var issues = await youTrackClient.GetIssuesAsync(cts.Token);
Dictionary<string, YouTrackChannelInfo> youTrackChannels = Utils.MapChannel(await youTrackClient.GetColumns(project, cts.Token));
Dictionary<string, Dictionary<string, Issue>> youTrackColumns = (await youTrackClient.GetIssueOnBoard(project, cts.Token))
    .ToDictionary(
        x => x.Columns.ID, 
        y => y.Issues.ToDictionary(
            x => x.ID, 
            z => z));
;

foreach (var issue in issues)
{
    if (!subChannel.Any(x => x.Id.Equals(issue.IDReadable.ToLower())))
    {
        ChannelInfo? newChannel = await Utils.CreateNewIssueChannel(
            youTrackClient,
            discord, 
            swp, 
            subChannel,
            youTrack, 
            issue, 
            cts.Token);

        if (newChannel != null)
        {
            subChannel = subChannel.Append(newChannel).ToArray()!;
        }
    }
}

subChannel = (await discord.GetChannelAsync(swp, cts.Token))?
    .Where(x => x.ParentId?.Equals(youTrack.Id) ?? false)
    ?.ToArray();

;
var job = new RecurringJob(TimeSpan.FromMilliseconds(10000));
job.Start(async (cancellationToken) =>
{
    
    Dictionary<string, Dictionary<string, Issue>> newYouTrackColumns = (await youTrackClient.GetIssueOnBoard(project, cancellationToken))
        .ToDictionary(
            x => x.Columns.ID, 
            y => y.Issues.ToDictionary(
                x => x.ID, 
                z => z));

    List<string> movedIssues = new List<string>();
    List<string> newIssues = new List<string>();

    foreach (string columnID in newYouTrackColumns.Keys)
    {
        var columnInfo = youTrackChannels[columnID];
        var currentIssues = newYouTrackColumns[columnID];
        var previewIssues = youTrackColumns[columnID];

        foreach (var issueKey in currentIssues.Keys)
        {
            if (!previewIssues.ContainsKey(issueKey))
            {   
                 newIssues.Add(issueKey);
            }
        }

        foreach (var issueKey in previewIssues.Keys)
        {
            if (!currentIssues.ContainsKey(issueKey))
            {
                movedIssues.Add(issueKey);
            }
        }
        
        List<string> movedIssuesToRemove = new List<string>();
        foreach (var issueKey in movedIssues)
        {
            if (currentIssues.ContainsKey(issueKey))
            {
                Issue issue = currentIssues[issueKey];
                ChannelInfo issueChannel = subChannel.First(x => x.Name.Equals(issue.IDReadable.ToLower()));
                await Utils.SendTaskMove(
                    discord, 
                    issueChannel,
                    issue,
                    columnInfo.Title, 
                    cancellationToken);

                movedIssuesToRemove.Remove(issueKey);
            }
        }

        foreach (var issueKey in movedIssuesToRemove)
        {
            movedIssues.Remove(issueKey);
        }

        List<string> issuesToRemove = new List<string>();
        foreach (var issueKey in newIssues)
        {
            Issue newIssue = currentIssues[issueKey];
            ChannelInfo? newChannel = await Utils.CreateNewIssueChannel(
                youTrackClient,
                discord, 
                swp, 
                subChannel,
                youTrack, 
                newIssue, 
                cancellationToken);

            if (newChannel != null)
            {
                subChannel = subChannel.Append(newChannel).ToArray()!;
            }
            
            issuesToRemove.Add(issueKey);
        }

        foreach (var issueKey in issuesToRemove)
        {
            newIssues.Remove(issueKey);
        }
    }

    youTrackColumns = newYouTrackColumns;
});

await Task.Delay(-1);