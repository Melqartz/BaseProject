using BaseProject.Areas.Admin.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Areas.Admin.Controllers;

public class HomeController(ILogger<HomeController> logger) : AdminBaseController {
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index() {
        return View();
    }

    [AllowAnonymous]
    public IActionResult Privacy() {
        return View();
    }
}