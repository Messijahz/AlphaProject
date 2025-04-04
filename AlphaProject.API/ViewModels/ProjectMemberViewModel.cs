namespace AlphaProject.API.ViewModels;

public class ProjectMemberViewModel
{
    public string Id { get; set; } = null!;
    public string FullName { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; } = "/images/profile-icon-small-1.svg";
}
