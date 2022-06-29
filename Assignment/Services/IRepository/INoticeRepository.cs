using Assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.IRepository;

public interface INoticeRepository
{
    Task Create(Notice model);
    Task<Notice> Details(int id, string uId);
    Task Save();

}