using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Ejercicio3.Models;

namespace Ejercicio3.Controllers;

public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) {
        _logger = logger;
    }

    public IActionResult Index() {
        ViewData["ActivePage"] = "Home"; // Indicamos que la página activa es Alumno. Con esto podemos aplicar la clase 'active' al link en el navbar.
        return View();
    }

    public IActionResult Privacy() {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}