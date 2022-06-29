using Assignment.Models.ViewModels;

namespace Assignment.Services.IRepository;

public interface IAuthRepository
{
    Task<ResponseVm> Register(RegisterVm model);
    Task<ResponseVm> Login(LoginVm model);
    Task Logout();
    void Save();

}