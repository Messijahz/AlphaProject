using Microsoft.AspNetCore.Identity;

namespace AlphaProject.Core.Models;

public class AppUser : IdentityUser
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
    public string? FullName { get; set; }

    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
    public string? ProfileImageUrl { get; set; }
}
