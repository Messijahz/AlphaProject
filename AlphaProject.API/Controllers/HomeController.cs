using Microsoft.AspNetCore.Mvc;

namespace AlphaProject.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Home";

        return View();
    }
}
