using BaseProject.Areas.Admin.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Areas.Admin.Controllers;

[AllowAnonymous]
public class AccountController(ILogger<AccountController> logger) : AdminBaseController {
    private readonly ILogger<AccountController> _logger = logger;

    #region Views

    public Task<IActionResult> Login(string? returnUrl = null) {
        ViewData["ReturnUrl"] = returnUrl;
        return Task.FromResult<IActionResult>(View());
    }
    
    public Task<IActionResult> Signup(string? returnUrl = null) {
        ViewData["ReturnUrl"] = returnUrl;
        return Task.FromResult<IActionResult>(View());
    }
    
    public Task<IActionResult> ResetPassword(string? returnUrl = null) {
        ViewData["ReturnUrl"] = returnUrl;
        return Task.FromResult<IActionResult>(View());
    }
    
    public Task<IActionResult> ForgotPassword(string? returnUrl = null) {
        ViewData["ReturnUrl"] = returnUrl;
        return Task.FromResult<IActionResult>(View());
    }

    #endregion
}