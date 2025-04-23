using AlphaProject.API.ViewModels;
using AlphaProject.Core.Models;
using AlphaProject.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphaProject.API.Controllers;

[Authorize]
[Route("projects")]
public class ProjectsController : Controller
{

    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }


    [HttpGet("")]
    public async Task<IActionResult> Index(Guid? statusId)
    {
        var allProjects = await _projectService.GetAllAsync();

        var filteredProjects = allProjects;

        if (statusId.HasValue)
        {
            filteredProjects = allProjects.Where(p => p.StatusId == statusId.Value);
        }

        var viewModel = new ProjectListViewModel
        {
            AllProjects = allProjects.ToList(),
            FilteredProjects = filteredProjects.ToList()
        };

        return View(viewModel);
    }



    [HttpGet("details/{id}")]
    public IActionResult Details(Guid id)
    {
        return View();
    }



    [HttpGet("create")]
    [Authorize]
    public IActionResult Create()
    {
        var form = new ProjectFormModel
        {
            AvailableMembers = new List<ProjectMemberViewModel> 
            {
                new ProjectMemberViewModel { Id = Guid.NewGuid().ToString(), FullName = "Theresé Lidbom"},
                new ProjectMemberViewModel { Id = Guid.NewGuid().ToString(), FullName = "Hans-Mattin Lassei"},
            }
        };
        return View(form);
    }



    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProjectFormModel form)
    {
        if (!ModelState.IsValid)
        {
            form.AvailableMembers = new List<ProjectMemberViewModel>
            {
                new ProjectMemberViewModel { Id = Guid.NewGuid().ToString(), FullName = "Theresé Lidbom"},
                new ProjectMemberViewModel { Id = Guid.NewGuid().ToString(), FullName = "Hans-Mattin Lassei"},
            };

            return View(form);
        }

        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            Name = form.ProjectName,
            Description = form.Description ?? "",
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            Budget = form.Budget ?? 0,
            CustomerId = Guid.Parse("c5e101be-7c0c-449e-8390-f13a6e334c37"),
            ProjectManagerId = Guid.Parse("7153068e-1140-45f9-8b9f-2713d2f5dac2"),
            StatusId = Guid.Parse("44444444-4444-4444-4444-444444444444")
        };

        await _projectService.CreateProjectAsync(project);

        return RedirectToAction("Index");
    }



    [HttpGet("edit/{id}")]
    public IActionResult Edit(Guid id)
    {

        var form = new ProjectFormModel
        {
            AvailableMembers = new List<ProjectMemberViewModel>
            {
                new ProjectMemberViewModel { Id = Guid.NewGuid().ToString(), FullName = "Theresé Lidbom"},
                new ProjectMemberViewModel { Id = Guid.NewGuid().ToString(), FullName = "Hans-Mattin Lassei"},
            }
        };

        return View(form);
    }

    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ProjectFormModel form)
    {
        if (!ModelState.IsValid)
        {
            form.AvailableMembers = new List<ProjectMemberViewModel>
            {
                new ProjectMemberViewModel { Id = Guid.NewGuid().ToString(), FullName = "Theresé Lidbom"},
                new ProjectMemberViewModel { Id = Guid.NewGuid().ToString(), FullName = "Hans-Mattin Lassei"},
            };

            return View(form);
        }


        return RedirectToAction("Index");
    }

    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _projectService.SoftDeleteProjectAsync(id);
        return RedirectToAction("Index");
    }
}
