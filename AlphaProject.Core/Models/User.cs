using System.ComponentModel.DataAnnotations;

namespace AlphaProject.Core.Models;

public class User
{
    [Key]
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;

    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

}
