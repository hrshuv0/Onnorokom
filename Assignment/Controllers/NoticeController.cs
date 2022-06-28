using Assignment.Data;
using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace Assignment.Controllers;

public class NoticeController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public NoticeController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Notice model)
    {
        if (!ModelState.IsValid)
            return View(model);
        
        _dbContext.Notices!.Add(model);
        await _dbContext.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index), "Home");
    }

}