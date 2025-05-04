using System.ComponentModel.DataAnnotations;

namespace AlphaProject.Core.Models;

public class Project
{
    [Key]
    public Guid ProjectId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal Budget { get; set; }

    public Guid StatusId { get; set; }
    public Status? Status { get; set; }

    public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
    public bool IsDeleted { get; set; } = false;

}
