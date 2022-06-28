using System.Security.Claims;
using Assignment.Data;
using Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public async Task<IActionResult> Details(int id)
    {
        var notice = _dbContext.Notices!
            .Include(s => s.Details)!
            .ThenInclude(t => t.ApplicationUser)
            .FirstOrDefault(x => x.NoticeId == id);
        
        var uId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = _dbContext.Users.FirstOrDefault(x => x.Id == uId);

        var userExists = notice.Details.FirstOrDefault(x => x.ApplicationUser.Id == uId);
        if (userExists is null)
        {
            notice.Details.Add(new NoticeDetails()
            {
                ApplicationUser = user,
                NoticeId = id,
                HitCount = 1
            });
        }
        else
        {
            userExists.HitCount += 1;
        }

        await _dbContext.SaveChangesAsync();

        return View(notice);
    }
}