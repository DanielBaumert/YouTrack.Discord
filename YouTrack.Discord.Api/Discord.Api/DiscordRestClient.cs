using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YouTrack.Discord.Api.Discord.Api;

public class DiscordRestClient
{
    private static readonly JsonSerializerOptions s_jsonSerializerOptions = new JsonSerializerOptions
    {
        IncludeFields = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private const string BASE_URL = "https://discord.com/api/v10";
    private const string CREATE_MESSAGE = $"{BASE_URL}/channels/{{0}}/messages";
    private const string CREATE_CHANNEL = $"{BASE_URL}/guilds/{{0}}/channels";

    private readonly HttpClient _httpClient;
    private DiscordRestClient(HttpClient client)
    {
        _httpClient = client;
    }

  
    public static DiscordRestClient Create(string token)
    {
        HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("user-agent", "DiscordBot");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", token);
        
        return new DiscordRestClient(httpClient);
    }
    public async Task<Guild[]> GetGuildsAsync(CancellationToken cancellationToken = default)
    {
        var res = await _httpClient.GetAsync(
            Path.Combine(BASE_URL, "users/@me/guilds"), 
            cancellationToken);
        
        res = res.EnsureSuccessStatusCode();
        if (res.IsSuccessStatusCode)
        {
            var data = await res.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<Guild[]>(data, s_jsonSerializerOptions)!;
        }

        throw new Exception("Connection to the Discord-Api failed");
    }

    public async Task<ChannelInfo[]> GetChannelAsync(Guild guild, CancellationToken cancellationToken = default, [CallerMemberName] string srcCall = "")
    {
        var res = await _httpClient.GetAsync(
            Path.Combine(BASE_URL, $"guilds/{guild.Id}/channels"),
            cancellationToken);

        res = res.EnsureSuccessStatusCode();
        if (res.IsSuccessStatusCode)
        {
            var data = await res.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<ChannelInfo[]>(data, s_jsonSerializerOptions)!;
        }

        throw new Exception("Connection to the Discord-Api failed");
    }

    public async Task<ChannelInfo> CreateChannelAsync(Guild guild, ChannelCreateInfo channelCreateInfo, CancellationToken cancellationToken = default)
    {
        string target = string.Format(CREATE_CHANNEL, guild.Id);
        var res = await _httpClient.PostAsJsonAsync(
            target,
            channelCreateInfo, 
            s_jsonSerializerOptions,
            cancellationToken);

        res = res.EnsureSuccessStatusCode();
        if (res.IsSuccessStatusCode)
        {
            var data = await res.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<ChannelInfo>(data, s_jsonSerializerOptions)!;
        }

        throw new Exception("Connection to the Discord-Api failed");
    }

    public async Task<Message[]> GetChannelMessagesAsync(ChannelInfo channel, MessageRequestInfo messageRequestInfo, CancellationToken cancellationToken = default)
    {
        var target = string.Concat(string.Format(CREATE_MESSAGE, channel.Id), "?", WebUtils.Serialize(messageRequestInfo));
        var res = await _httpClient.GetAsync(target, cancellationToken);

        res = res.EnsureSuccessStatusCode();
        if (res.IsSuccessStatusCode)
        {
            var data = await res.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<Message[]>(data, s_jsonSerializerOptions)!;
        }

        throw new Exception("Connection to the Discord-Api failed");
    }

    public async Task SendChannelMessageAsync(ChannelInfo channel, MessageCreateInfo messageCreateInfo, CancellationToken cancellationToken = default)
    {
        var target = string.Format(CREATE_MESSAGE, channel.Id);
        var res = await _httpClient.PostAsJsonAsync(
            target,
            messageCreateInfo,
            s_jsonSerializerOptions,
            cancellationToken);

        res = res.EnsureSuccessStatusCode();

        if (res.IsSuccessStatusCode)
        {
            var data = await res.Content.ReadAsStringAsync(cancellationToken);
        }
    }
}