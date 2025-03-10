namespace AlphaProject.Core.Models;

public class ProjectManager
{
    public Guid ProjectManagerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}
