using AlphaProject.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphaProject.API.Controllers;

[Authorize]
[Route("projects")]
public class ProjectsController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
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
}
