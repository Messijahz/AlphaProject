using Microsoft.AspNetCore.Mvc;

namespace AlphaProject.API.Controllers;

[Route("members")]
public class MembersController : Controller
{
    [Route("")]
    public IActionResult Members()
    {
        return View();
    }
    [HttpGet("details/{id}")]
    public IActionResult Details()
    {
        return View();
    }
    [HttpGet("create/{id}")]
    public IActionResult Create()
    {
        return View();
    }
    [HttpGet("edit/{id}")]
    public IActionResult Edit()
    {
        return View();
    }
}
