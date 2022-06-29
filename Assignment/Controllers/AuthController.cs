using Assignment.Models.ViewModels;
using Assignment.Services.IRepository;

using Microsoft.AspNetCore.Mvc;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace Assignment.Controllers;

public class AuthController : Controller
{
    private readonly IAuthRepository _authRepo;

    public AuthController(IAuthRepository authRepo)
    {
        _authRepo = authRepo;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVm model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _authRepo.Register(model);

        if (result.Status == Status.Success)
        {
            return RedirectToAction(nameof(Index), "Home");
        }
        
        TempData["Error"] = result.Message;
        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginVm model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _authRepo.Login(model);

        if (result.Status == Status.Success)
        {
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["Error"] = result.Message;
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _authRepo.Logout();
        return RedirectToAction(nameof(Index), "Home");
    }
    
    
    public IActionResult AccessDenied()
    {
        return View();
    }
    
}