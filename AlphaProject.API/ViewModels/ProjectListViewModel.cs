using AlphaProject.Core.Models;

namespace AlphaProject.API.ViewModels;

public class ProjectListViewModel
{
    public List<Project> AllProjects { get; set; } = new ();
    public List<Project> FilteredProjects { get; set; } = new ();
    public ProjectFormModel AddProjectForm { get; set; } = new ProjectFormModel();
}
