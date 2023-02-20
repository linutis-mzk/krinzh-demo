using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;

namespace Krinzh.Models;

public class MStreamerCard
{
    [Key]
    [JsonProperty("time_updated")]
    public DateTime time_updated { get; set; }
    [JsonProperty("user_name")]
    public string user_name { get; set; }
    [JsonProperty("custom_alias")]
    public string custom_alias { get; set; }
    [JsonProperty("is_online")]
    public bool is_online { get; set; }
    [JsonProperty("profile_image_url")]
    public string profile_image_url { get; set; }

}