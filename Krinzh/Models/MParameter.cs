using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Krinzh.Models;


[Table("parameters", Schema = "dbo")]
public class MParameter
{
    [Key]
    public string pr_key { get; set; }
    public string pr_value { get; set; }
}