using Newtonsoft.Json;

namespace TwitchAPI.Models;

public class MUserInfo
{
    [JsonProperty("id")]
    public string id { get; set; }
    [JsonProperty("login")]
    public string login { get; set; }
    [JsonProperty("display_name")]
    public string display_name { get; set; }
    [JsonProperty("type")]
    public string type { get; set; }
    [JsonProperty("broadcaster_type")]
    public string broadcaster_type { get; set; }
    [JsonProperty("description")]
    public string description { get; set; }
    [JsonProperty("profile_image_url")]
    public string profile_image_url { get; set; }
    [JsonProperty("offline_image_url")]
    public string offline_image_url { get; set; }
    [JsonProperty("view_count")]
    public int view_count { get; set; }
    [JsonProperty("created_at")]
    public DateTime created_at { get; set; }
}

public class MUsers
{
    [JsonProperty("data")]
    public List<MUserInfo> data { get; set; }
}