using Assignment.Data;
using Assignment.Data.Static;
using Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class StatsController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public StatsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET
    public IActionResult Index()
    {
        var stats = new Stats();

        stats.Details = _dbContext.NoticeDetails!
            .Include(x => x.ApplicationUser)
            .Include(x => x.Notice)
            .ToList();
        
        return View(stats);
    }
}