using BaseProject.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers;

[AllowAnonymous]
public class AccountController(ILogger<AccountController> logger) : BaseController {
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
    
    #region Actions
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null) {
        if (!ModelState.IsValid) {
            return View(model);
        }
        
        var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded) {
            _logger.LogInformation("User logged in.");
            return RedirectToLocal(returnUrl);
        }
        
        if (result.RequiresTwoFactor) {
            return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
        }
        
        if (result.IsLockedOut) {
            _logger.LogWarning("User account locked out.");
            return RedirectToAction(nameof(Lockout));
        }
        
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Signup(SignupViewModel model, string? returnUrl = null) {
        if (!ModelState.IsValid) {
            return View(model);
        }
        
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await UserManager.CreateAsync(user, model.Password);
        if (result.Succeeded) {
            _logger.LogInformation("User created a new account with password.");
            return RedirectToLocal(returnUrl);
        }
        
        foreach (var error in result.Errors) {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model, string? returnUrl = null) {
        if (!ModelState.IsValid) {
            return View(model);
        }
        
        var user = await UserManager.FindByEmailAsync(model.Email);
        if (user == null) {
            ModelState.AddModelError(string.Empty, "Invalid email.");
            return View(model);
        }
        
        var result = await UserManager.ResetPasswordAsync(user, model.Code, model.Password);
        if (result.Succeeded) {
            _logger.LogInformation("User reseted password.");
            return RedirectToLocal(returnUrl);
        }
        
        foreach (var error in result.Errors) {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model, string? returnUrl = null) {
        if (!ModelState.IsValid) {
            return View(model);
        }
        
        var user = await UserManager.FindByEmailAsync(model.Email);
        if (user == null) {
            ModelState.AddModelError(string.Empty, "Invalid email.");
            return View(model);
        }
        
        var code = await UserManager.GeneratePasswordResetTokenAsync(user);
        var callbackUrl = Url.Action(nameof(ResetPassword), "Account", new { code }, Request.Scheme);
        await EmailSender.SendEmailAsync(model.Email, "Reset Password", $"Please reset your password by <a href='{callbackUrl}'>clicking here</a>.");
        
        _logger.LogInformation("User forgot password.");
        return RedirectToLocal(returnUrl);
    }
    
    public IActionResult Lockout() {
        return View();
    }
    
    public async Task<IActionResult> Logout() {
        await SignInManager.SignOutAsync();
        _logger.LogInformation("User logged out.");
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
    
    #endregion
}