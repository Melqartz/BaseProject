using System.Diagnostics;
using BaseProject.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using BaseProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace BaseProject.Controllers;

[Authorize(Roles = "User")]
public class HomeController(ILogger<HomeController> logger) : BaseController {
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index() {
        return View();
    }

    [AllowAnonymous]
    public IActionResult Privacy() {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    public IActionResult ThrowException() {
        throw new Exception("This is an exception thrown from the HomeController.");
    }

    public IActionResult TiggerCatch() {
        var a = 0;
        var b = 10;
        var c = b / a;
        
        return Ok("Error logged.");
    }
}