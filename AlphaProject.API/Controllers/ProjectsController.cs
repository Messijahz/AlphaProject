﻿using Microsoft.AspNetCore.Mvc;

namespace AlphaProject.API.Controllers;

[Route("projects")]
public class ProjectsController : Controller
{
    [Route("")]
    public IActionResult Projects()
    {
        return View();
    }
    [HttpGet("details/{id}")]
    public IActionResult Details()
    {
        return View();
    }
    [HttpGet("create")]
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
