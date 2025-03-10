namespace AlphaProject.Core.Models;

public class ChangeLog
{
    public Guid LogId { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid ProjectId { get; set; }
    public Project Project { get; set; }

    public string ActionType { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public string OldData { get; set; }
    public string NewData { get; set; }
}
