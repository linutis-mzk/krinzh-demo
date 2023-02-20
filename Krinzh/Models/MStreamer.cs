using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Krinzh.Models;

public class MStreamer
{
    [Key]
    [JsonProperty("user_id")]
    public string user_id { get; set; }
    
    
    // Streamer's login name
    [JsonProperty("user_name")]
    public string user_name { get; set; }
    
    // Streamer's chosen display name
    [JsonProperty("display_name")]
    public string display_name { get; set; }
    
    // Krinzh user's chosen display name for the streamer
    [JsonProperty("custom_alias")]
    public string custom_alias { get; set; }
    
    
    [JsonProperty("is_online")]
    public bool is_online { get; set; }
    [JsonProperty("profile_image_url")]
    public string profile_image_url { get; set; }

}