using Newtonsoft.Json;

namespace TwitchAPI.Models;
public class MStreamerStatus
{
    [JsonProperty("id")]
    public string id { get; set; }
    [JsonProperty("user_id")]
    public string user_id { get; set; }
    [JsonProperty("user_login")]
    public string user_login { get; set; }
    [JsonProperty("user_name")]
    public string user_name { get; set; }
    [JsonProperty("game_id")]
    public string game_id { get; set; }
    [JsonProperty("game_name")]
    public string game_name { get; set; }
    [JsonProperty("type")]
    public string type { get; set; }
    [JsonProperty("title")]
    public string title { get; set; }
    [JsonProperty("viewer_count")]
    public int viewer_count { get; set; }
    [JsonProperty("started_at")]
    public DateTime started_at { get; set; }
    [JsonProperty("language")]
    public string language { get; set; }
    [JsonProperty("thumbnail_url")]
    public string thumbnail_url { get; set; }
    [JsonProperty("tag_ids")]
    public List<string> tag_ids { get; set; }
    [JsonProperty("tags")]
    public List<string> tags { get; set; }
    [JsonProperty("is_mature")]
    public bool is_mature { get; set; }
}

public class Pagination
{
    [JsonProperty("cursor")]
    public string cursor { get; set; }
}

public class MStreamers
{
    [JsonProperty("data")]
    public List<MStreamerStatus> data { get; set; }
    [JsonProperty("pagination")]
    public Pagination pagination { get; set; }
}