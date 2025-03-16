using Microsoft.AspNetCore.Mvc;

namespace AlphaProject.API.Controllers;

[Route("projects")]
public class ProjectsController : Controller
{
    [Route("")]
    public IActionResult Projects()
    {
        return View();
    }
}
