using Assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.IRepository;

public interface INoticeRepository
{
    Task<List<Notice>> GetNoticeList();
    Task Create(Notice model);
    Task<Notice> Details(int id, string uId);
    Task Save();

}