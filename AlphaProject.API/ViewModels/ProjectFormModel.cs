using System.ComponentModel.DataAnnotations;

namespace AlphaProject.API.ViewModels;

public class ProjectFormModel
{
    [Display(Name = "Project Image")]
    public IFormFile? ProjectImg { get; set; }

    [Required(ErrorMessage = "Project Name is required.")]
    [Display(Name = "Project Name")]
    public string ProjectName { get; set; } = null!;


    [Required(ErrorMessage = "Client Name is required.")]
    [Display(Name = "Client Name")]
    public string ClientName { get; set; } = null!;


    [Display(Name = "Description")]
    public string? Description { get; set; }


    [Required(ErrorMessage = "Start Date is required.")]
    [Display(Name = "Start Date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }


    [Required(ErrorMessage = "End Date is required.")]
    [Display(Name = "End Date")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }


    [Display(Name = "Members")]
    public List<Guid> Members { get; set; } = new();


    [Display(Name = "Budget")]
    public decimal? Budget { get; set; }

    public List<ProjectMemberViewModel> AvailableMembers { get; set; } = new();
}
