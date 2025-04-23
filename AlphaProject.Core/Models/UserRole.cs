using System.ComponentModel.DataAnnotations;

namespace AlphaProject.Core.Models;

public class UserRole
{
    [Key]
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
}
