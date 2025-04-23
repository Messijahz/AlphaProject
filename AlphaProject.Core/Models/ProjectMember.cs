using System.ComponentModel.DataAnnotations;

namespace AlphaProject.Core.Models;

public class ProjectMember
{
    [Key]
    public Guid MemberId { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
}
