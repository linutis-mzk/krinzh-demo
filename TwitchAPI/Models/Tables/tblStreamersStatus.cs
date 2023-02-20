using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;

namespace TwitchAPI.Models;

/// <summary>
/// A model representing schema of the streamers_status table. Used to store
/// time and online status of streamers.
/// </summary>
[Table("streamers_status", Schema = "dbo")]
public class tblStreamersStatus
{
    public DateTime time_updated { get; set; }
    
    public string user_id { get; set; }
    
    public string game_id {get; set; }
    
    public int viewer_count { get; set; }
    
    public bool is_online { get; set; }
    
    public DateTime stream_start_time { get; set; }
    
    public DateTime stream_end_time { get; set; }
    

}

