using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;

namespace TwitchAPI.Models;

/// <summary>
/// A model representing schema of the tokens table. Used to store
/// twitch access token information.
/// </summary>
[Table("tokens", Schema = "dbo")]
public class tblTokens
{
    public string owner { get; set; }
    
    public DateTime time_updated { get; set; }
    
    public string access_token { get; set; }
    
    public int expires_in { get; set; }
    
    public string token_type { get; set; }
}

