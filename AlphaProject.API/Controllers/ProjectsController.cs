﻿using AlphaProject.API.ViewModels;
using AlphaProject.Core.Models;
using AlphaProject.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlphaProject.API.Controllers;

[Authorize]
[Route("projects")]
public class ProjectsController : Controller
{

    private readonly IProjectService _projectService;
    private readonly UserManager<AppUser> _userManager;

    public ProjectsController(IProjectService projectService, UserManager<AppUser> userManager)
    {
        _projectService = projectService;
        _userManager = userManager;
    }

    private List<ProjectMemberViewModel> GetTestMember()
    {
        return new List<ProjectMemberViewModel>()
        {
            new ProjectMemberViewModel {Id = Guid.NewGuid().ToString(), FullName = "Andreas Laine" },
            new ProjectMemberViewModel { Id = Guid.NewGuid().ToString(), FullName = "Kristoffer Test" },
        };
    }


    [HttpGet("{status?}")]
    public async Task<IActionResult> Index(string? status, string? query)
    {
        var allProjects = await _projectService.GetAllAsync();
        var filteredProjects = allProjects;

        if (!string.IsNullOrEmpty(status))
        {
            if (status.ToLower() == "started")
            {
                filteredProjects = filteredProjects.Where(p => p.StatusId == Guid.Parse("55555555-5555-5555-5555-555555555555"));
            }
            else if (status.ToLower() == "completed")
            {
                filteredProjects = filteredProjects.Where(p => p.StatusId == Guid.Parse("66666666-6666-6666-6666-666666666666"));
            }
        }

        if (!string.IsNullOrWhiteSpace(query))
        {
            filteredProjects = filteredProjects.Where(p =>
                (!string.IsNullOrEmpty(p.Name) && p.Name.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(p.ClientName) && p.ClientName.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(p.Description) && p.Description.Contains(query, StringComparison.OrdinalIgnoreCase))
            );
        }

        var viewModel = new ProjectListViewModel
        {
            AllProjects = allProjects.ToList(),
            FilteredProjects = filteredProjects.ToList(),
            AddProjectForm = new ProjectFormModel
            {
                AvailableMembers = GetTestMember()
            }
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
            AvailableMembers = GetTestMember()
        };
        return PartialView("Partials/Modals/_AddProjectModal", form);
    }



    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProjectFormModel form)
    {
        if (!ModelState.IsValid)
        {
            form.AvailableMembers = GetTestMember();

            var allProjects = await _projectService.GetAllAsync();

            var viewModel = new ProjectListViewModel
            {
                AllProjects = allProjects.ToList(),
                FilteredProjects = allProjects.ToList(),
                AddProjectForm = form
            };

            return View("Index", viewModel);
        }

        var projectId = Guid.NewGuid();
        var project = new Project
        {
            ProjectId = projectId,
            Name = form.ProjectName,
            ClientName = form.ClientName,
            Description = form.Description ?? "",
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            Budget = form.Budget ?? 0,
            StatusId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            ProjectMembers = form.Members.Select(m => new ProjectMember
            {
                MemberId = m,
                ProjectId = projectId
            }).ToList()
        };

        await _projectService.CreateProjectAsync(project);

        return RedirectToAction("Index");
    }



    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(Guid id)
    {

        var project = await _projectService.GetByIdAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        var form = new ProjectFormModel
        {
            ProjectId = project.ProjectId,
            ProjectName = project.Name,
            ClientName = project.ClientName,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate ?? DateTime.MinValue,
            Budget = project.Budget,
            Members = project.ProjectMembers.Select(pm => pm.MemberId).ToList(),
            AvailableMembers = GetTestMember()
        };

        return PartialView("Partials/Forms/_EditProjectForm", form);
    }

    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ProjectFormModel form)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index", "Projects");
        }

        var existingProject = await _projectService.GetByIdAsync(id);

        if (existingProject == null)
        {
            return NotFound();
        }

        existingProject.Name = form.ProjectName;
        existingProject.ClientName = form.ClientName;
        existingProject.Description = form.Description ?? "";
        existingProject.StartDate = form.StartDate;
        existingProject.EndDate = form.EndDate;
        existingProject.Budget = form.Budget ?? 0;

        existingProject.ProjectMembers.Clear();
        existingProject.ProjectMembers = form.Members.Select(m => new ProjectMember
        {
            MemberId = m,
            ProjectId = existingProject.ProjectId
        }).ToList();

        await _projectService.UpdateProjectAsync(existingProject);

        return RedirectToAction("Index", "Projects");
    }



    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _projectService.SoftDeleteProjectAsync(id);
        return RedirectToAction("Index");
    }
}
