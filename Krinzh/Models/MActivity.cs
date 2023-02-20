using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Krinzh.Models;

[Table("activities", Schema = "user_data")]
public class MActivity
{
    [Key]
    public string id { get; set; }
    public int position { get; set; }
    public DateTime date { get; set; }
    public string category { get; set; }
    public string description { get; set; }
    public string description_extra { get; set; }
    public bool important { get; set; }
    public bool done { get; set; }
}