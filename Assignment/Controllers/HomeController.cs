using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Assignment.Models;
using Assignment.Services.IRepository;

namespace Assignment.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly INoticeRepository _noticeRepo;

    public HomeController(ILogger<HomeController> logger, INoticeRepository noticeRepo)
    {
        _logger = logger;
        _noticeRepo = noticeRepo;
    }
    
    public async Task<IActionResult> Index()
    {
        var result = await _noticeRepo.GetNoticeList();
        
        return View(result);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}