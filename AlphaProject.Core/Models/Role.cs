using System.ComponentModel.DataAnnotations;

namespace AlphaProject.Core.Models;

public class Role
{
    [Key]
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
}
