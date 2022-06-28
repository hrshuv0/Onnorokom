using System.Diagnostics;
using Assignment.Data;
using Microsoft.AspNetCore.Mvc;
using Assignment.Models;
using Microsoft.AspNetCore.Authorization;

namespace Assignment.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        // var notice = _dbContext.Notices!
        //     .OrderByDescending(n => n.NoticeId)
        //     .ToList();
        
        var unseen = (from notice in _dbContext.Notices
            where !(from nd in _dbContext.NoticeDetails
                    select nd.NoticeId)
                .Contains(notice.NoticeId)
            select notice).ToList();

        var seen = (from notice in _dbContext.Notices
            join noticeDetails in _dbContext.NoticeDetails
                on notice.NoticeId equals noticeDetails.NoticeId
            select notice).ToList();

        var result = new List<Notice>();
        result.AddRange(unseen.DistinctBy(x=>x.NoticeId));
        result.AddRange(seen.DistinctBy(x=>x.NoticeId));
        
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