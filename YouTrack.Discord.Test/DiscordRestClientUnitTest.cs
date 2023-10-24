using System.Drawing;
using YouTrack.Discord.Api.Discord.Api;

namespace YouTrack.Discord.Test;

[TestClass]
public class DiscordRestClientUnitTest
{
    const string BOT_TOKEN = "[DISCORD-BOT-TOKEN]";

    private DiscordRestClient _discord;
    private CancellationTokenSource cts;
    private Guild _testServer;
    private ChannelInfo _testChannel;

    [TestInitialize]
    public async Task DiscordRestClientInit()
    {
        cts = new CancellationTokenSource();
        _discord = DiscordRestClient.Create(BOT_TOKEN);
        
        var guilds = (await _discord.GetGuildsAsync(cts.Token)) ?? throw new Exception();
        _testServer = guilds?.FirstOrDefault(x => x.Name.Equals("Isa-0xNull")) ?? throw  new Exception("Guild not found");

        var channels = (await _discord.GetChannelAsync(_testServer, cts.Token)) ?? throw new Exception("Channels not found");
        _testChannel = channels.FirstOrDefault(x => x.Id.Equals("1039584213659299920")) ?? throw  new Exception("Channel 1039584213659299920 not found");
    }


    [TestMethod]
    public async Task SendEmbets()
    {
        Embed embedInfo = new Embed
        {
            Type = EmbedType.Rich.ToString().ToLower(),
            Color = 0xff0000,
            Title = "Issue Title",
            Description = "Issue Desc.",
            Url = "https://www.google.de",
        };

        MessageCreateInfo messageCreateInfo = new MessageCreateInfo
        {
            Content = "Issue Info",
            Embeds = new Embed[]
            {
                embedInfo
            },
            Flags = null,
            MessageReference = null,
            TTS = false
        };

        await _discord.SendChannelMessageAsync(_testChannel, messageCreateInfo);        
    }
    
    [TestMethod]
    public async Task GetChannelMessages()
    {
        MessageRequestInfo messageRequestInfo = new MessageRequestInfo
        {
            Limit = 100
        };
        
        var channelMessages = await _discord.GetChannelMessagesAsync(_testChannel, messageRequestInfo);
    }
}