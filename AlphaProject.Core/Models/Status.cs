using System.ComponentModel.DataAnnotations;

namespace AlphaProject.Core.Models;

public class Status
{
    [Key]
    public Guid StatusId { get; set; }
    public string StatusName { get; set; } = string.Empty;
}
