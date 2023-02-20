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
[Table("parameters", Schema = "dbo")]
public class tblParameters
{
    public string pr_key { get; set; }
    
    public string pr_value { get; set; }

}

