using Assignment.Data;
using Assignment.Models;
using Assignment.Services.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Services;

public class NoticeRepository : INoticeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public NoticeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task Create(Notice model)
    {
        _dbContext.Notices!.Add(model);
        await _dbContext.SaveChangesAsync();
        await Save();
    }

    public async Task<Notice> Details(int id, string uId)
    {
        var notice = _dbContext.Notices!
            .Include(s => s.Details)!
            .ThenInclude(t => t.ApplicationUser)
            .FirstOrDefault(x => x.NoticeId == id);
        
        var user = _dbContext.Users.FirstOrDefault(x => x.Id == uId);

        var userExists =  notice.Details.FirstOrDefault(x => x.ApplicationUser.Id == uId);
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

        await Save();

        return notice;
    }

    public Task Save()
    {
        return _dbContext.SaveChangesAsync();
    }
}