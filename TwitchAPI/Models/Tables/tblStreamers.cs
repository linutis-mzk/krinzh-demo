using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;

namespace TwitchAPI.Models;

[Table("streamers", Schema = "dbo")]
public class tblStreamers
{
    public string user_id { get; set; }
    
    public string user_name { get; set; }
    
    public string display_name { get; set; }
    
    public string custom_alias { get; set; }
    
    public string profile_image_url { get; set; }
}

