using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP07_ROTH.Models;

namespace TP07_ROTH.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult CargarTareas()
    {
        return View();
    }
    public IActionResult CrearTareas()
    {
        return View();
    }
    public IActionResult CrearTareaGuardar()
    {
        return View();
    }
    public IActionResult FinalizarTarea()
    {
        return View();
    }
    public IActionResult EliminarTarea()
    {
        return View();
    }
    public IActionResult EditarTarea()
    {
        return View();
    }
    public IActionResult EditarTareaGuardar()
    {
        return View();
    }
}
