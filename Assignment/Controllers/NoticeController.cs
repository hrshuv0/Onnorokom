using System.Security.Claims;
using Assignment.Data;
using Assignment.Models;
using Assignment.Services.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace Assignment.Controllers;

[Authorize]
public class NoticeController : Controller
{
    private readonly ApplicationDbContext _dbContext;
    private readonly INoticeRepository _noticeRepo;

    public NoticeController(ApplicationDbContext dbContext, INoticeRepository noticeRepo)
    {
        _dbContext = dbContext;
        _noticeRepo = noticeRepo;
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

        await _noticeRepo.Create(model);

        return RedirectToAction(nameof(Index), "Home");
    }

    public async Task<IActionResult> Details(int id)
    {
        var uId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        
        
        // var notice = _dbContext.Notices!
        //     .Include(s => s.Details)!
        //     .ThenInclude(t => t.ApplicationUser)
        //     .FirstOrDefault(x => x.NoticeId == id);
        //
        //
        // var user = _dbContext.Users.FirstOrDefault(x => x.Id == uId);
        //
        // var userExists = notice.Details.FirstOrDefault(x => x.ApplicationUser.Id == uId);
        // if (userExists is null)
        // {
        //     notice.Details.Add(new NoticeDetails()
        //     {
        //         ApplicationUser = user,
        //         NoticeId = id,
        //         HitCount = 1
        //     });
        // }
        // else
        // {
        //     userExists.HitCount += 1;
        // }
        //
        // await _dbContext.SaveChangesAsync();

        var result = await _noticeRepo.Details(id, uId);
        
        return View(result);
    }
}