using System.Data;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace YouTrack.Discord.Api.YouTrack.Api;

public class YouTrackRestClient
{
    static JsonSerializerOptions s_jsonSerializerOptions = new JsonSerializerOptions
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };
    
    private readonly HttpClient _httpClient;
    public string Domain { get; private set; }
    public string API { get; private set; }
    private YouTrackRestClient(HttpClient client, string domain, string api)
    {
        _httpClient = client;
        Domain = domain;
        API = api;
    }

    public static YouTrackRestClient Create(string domain, string token)
    {
        HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("user-agent", "DiscordBot");
        httpClient.DefaultRequestHeaders.Add("accept", "application/json");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = true };

        if (!domain.EndsWith("/"))
        {
            domain = $"{domain}/";
        }  
        
        string api = $"{domain}api/";
        return new YouTrackRestClient(httpClient, domain, api);
    }

    public async Task<Project[]> GetProjectsAsync(CancellationToken cancellationToken = default)
    {
        string target = $"{API}agiles?fields=id,name,shortName";
        HttpResponseMessage res = await _httpClient.GetAsync(
            target,
            cancellationToken);

        res = res.EnsureSuccessStatusCode();
        if (res.IsSuccessStatusCode)
        {
            string data = await res.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<Project[]>(data, s_jsonSerializerOptions)
                ?? throw new Exception("Can not read the YouTrack response");
        }

        throw new Exception("Connection to the YouTrack-Api failed");
    }


    public async Task CreateIssue(
        Project project,
        Issue issue, 
        CancellationToken cancellationToken = default)
    {
        string target = $"{API}issues?fields=idReadable";
        IssueCreateInfo createInfo = new IssueCreateInfo()
        {
            Summary = issue.Summary,
            Description = issue.Description,
            Project = new ProjectInfo()
            {
                Id = "80-54"
            }
        };

        JsonContent ctx = JsonContent.Create(createInfo);
        HttpResponseMessage res = await _httpClient.PostAsync(
            target,
            ctx,
            cancellationToken);
        
        res.EnsureSuccessStatusCode();
    }

    public async Task<IssueTimeTracking?> GetTimeTrack(Issue issue, CancellationToken cancellationToken = default)
    {
            string target = $"{API}issues/{issue.IDReadable}/timeTracking?fields=workItems(textPreview,text,type(name),author(login,fullName,email),created(login,fullName,email),duration(minutes,presentation),date)";
        
        HttpResponseMessage res = await _httpClient.GetAsync(
            target,
            cancellationToken);

        res = res.EnsureSuccessStatusCode();
        if (res.IsSuccessStatusCode)
        {
            string data = await res.Content.ReadAsStringAsync(cancellationToken);
            IssueTimeTracking? track = JsonSerializer.Deserialize<IssueTimeTracking>(data);
            return track;
        }

        return null;
    }

    public async Task SetTimeTrack(Issue issue, IssueTimeTracking timeTracking, CancellationToken cancellationToken = default)
    {
        string target = $"{API}issues/{issue.IDReadable}/timeTracking";
        
        JsonContent content = JsonContent.Create(timeTracking);
        HttpResponseMessage res = await _httpClient.PostAsync(
            target,
            content,
            cancellationToken);

        res = res.EnsureSuccessStatusCode();
    }
    
    public async Task<Issue[]> GetIssuesAsync(CancellationToken cancellationToken = default)
    { 
        string target = $"{API}issues?fields=id,idReadable,summary,description,comments(id,text,author(id,login,fullName))";
        HttpResponseMessage res = await _httpClient.GetAsync(
            target,
            cancellationToken);

       res = res.EnsureSuccessStatusCode();
       if (res.IsSuccessStatusCode)
       {
           string data = await res.Content.ReadAsStringAsync(cancellationToken);
           return JsonSerializer.Deserialize<Issue[]>(data, s_jsonSerializerOptions)
               ?? throw new Exception("Can not read the YouTrack response");
       }

       throw new Exception("Connection to the YouTrack-Api failed");
    }

    public async Task<Column[]> GetColumns(
        Project project,
        CancellationToken cancellationToken)
    {
        string target = $"{API}agiles/{project.ID}?$top=-1&fields=columnSettings(id,columns(id,collapsed,ordinal,fieldValues(id,isResolved,ordinal,presentation)))";
        var res = await _httpClient.GetAsync(
            target,
            cancellationToken);

        res = res.EnsureSuccessStatusCode();
        if (res.IsSuccessStatusCode)
        {
            string data = await res.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonSerializer.Deserialize<ColumnsInfo>(data, s_jsonSerializerOptions);
            return result?.ColumnSettings.Columns 
                   ?? throw  new Exception("Can't get channels from YouTrack");
        }

        throw new Exception("Connection to the YouTrack-Api failed");
    }

    public async Task<Cell[]> GetIssueOnBoard(
        Project project,
        CancellationToken cancellationToken)
    {
        string target = $"{API}agiles/{project.ID}/sprints/current?fields=board(orphanRow(cells(issuesCount,issues(summary,idReadable,id,description),column(id))))";
        var res = await _httpClient.GetAsync(
            target,
            cancellationToken);

        res = res.EnsureSuccessStatusCode();
        if (res.IsSuccessStatusCode)
        {
            string data = await res.Content.ReadAsStringAsync(cancellationToken);
            JsonNode root = JsonNode.Parse(data) ?? throw new Exception("no json data available");
            return root["board"]?["orphanRow"]?["cells"].Deserialize<Cell[]>()
                   ?? throw new Exception("Can not read the YouTrack response");;
        }

        throw new Exception("Connection to the YouTrack-Api failed");
    }
}



public class Duration
{
    [JsonPropertyName("presentation")]
    public string Presentation { get; set; }

    [JsonPropertyName("minutes")]
    public int Minutes { get; set; }

    [JsonPropertyName("$type")]
    public string Type { get; set; }
}

public class WorkItemType
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("$type")]
    public string Type { get; set; }
}

public class WorkItem
{
    [JsonPropertyName("created")]
    public long Created { get; set; }

    [JsonPropertyName("date")]
    public long Date { get; set; }

    [JsonPropertyName("duration")]
    public Duration Duration { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("type")]
    public WorkItemType Type { get; set; }

    [JsonPropertyName("author")]
    public User Author { get; set; }
    
    [JsonPropertyName("creator")]
    public User Creator { get; set; }

    [JsonPropertyName("textPreview")]
    public string TextPreview { get; set; }
}