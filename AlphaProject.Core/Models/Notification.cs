using System.ComponentModel.DataAnnotations;

namespace AlphaProject.Core.Models;

public class Notification
{
    [Key]
    public Guid NotificationId { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid ProjectId { get; set; }
    public Project Project { get; set; }

    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsRead { get; set; } = false;
}
