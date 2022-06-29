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


    public async Task<List<Notice>> GetNoticeList()
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
        
        seen = seen.OrderByDescending(x => x.NoticeId).ToList();

        var result = new List<Notice>();
        result.AddRange(unseen.DistinctBy(x=>x.NoticeId));
        result.AddRange(seen.DistinctBy(x=>x.NoticeId));

        return  result;
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