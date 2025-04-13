﻿using System.ComponentModel.DataAnnotations;

namespace AlphaProject.Core.Models;

public class Project
{
    [Key]
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal Budget { get; set; }

    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }

    public Guid ProjectManagerId { get; set; }
    public ProjectManager ProjectManager { get; set; }

    public Guid StatusId { get; set; }
    public Status Status { get; set; }

    public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
}
