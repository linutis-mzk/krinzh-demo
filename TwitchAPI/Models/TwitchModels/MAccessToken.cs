using Newtonsoft.Json;

namespace TwitchAPI.Models;

public class MAccessToken
{
    [JsonProperty("access_token")]
    public string access_token { get; set; }

    [JsonProperty("expires_in")]
    public long expires_in { get; set; }

    [JsonProperty("token_type")]
    public string token_type { get; set; }
}