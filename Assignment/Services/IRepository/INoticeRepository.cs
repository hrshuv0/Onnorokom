using Assignment.Models;
using Assignment.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.IRepository;

public interface INoticeRepository
{
    Task<List<Notice>> GetNoticeList();
    Task<ResponseVm> Create(Notice model);
    Task<Notice>  Details(int id, string uId);
    Task Save();

}