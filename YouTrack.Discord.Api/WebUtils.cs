using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;

namespace YouTrack.Discord.Api;

public static class WebUtils
{
    public static string Serialize<T>(T data)
    {
        return string.Join("&", typeof(T).GetProperties()
            .Select(x
                => (x.Name, x.GetMethod?.Invoke(data, null) ?? null))
            .Where(x => x.Item2 != null)
            .Select(x => $"{x.Name.ToLower()}={UrlEncoder.Default.Encode(x.Item2.ToString())}"));
    }
}