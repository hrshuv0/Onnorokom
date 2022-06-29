using Assignment.Data.Static;
using Assignment.Models;
using Assignment.Models.ViewModels;
using Assignment.Services.IRepository;
using Microsoft.AspNetCore.Identity;

namespace Assignment.Services;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ResponseVm> Register(RegisterVm model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);

        var response = new ResponseVm();
        if (user is not null)
        {
            response.Status = Status.Failed;
            response.Message = "User name already exists";

            return response;
        }

        var newUser = new ApplicationUser()
        {
            UserName = model.UserName
        };
        var result = await _userManager.CreateAsync(newUser, model.Password);

        if (!result.Succeeded)
        {
            response.Status = Status.Failed;
            response.Message = "User registration failed";
            response.Message = result.Errors.FirstOrDefault().ToString();

            return response;
        }
        await _userManager.AddToRoleAsync(newUser, UserRoles.User);

        var loginStatus = await _signInManager.PasswordSignInAsync(newUser, model.Password, false, false);

        if (loginStatus.Succeeded)
        {
            response.Status = Status.Success;
            response.Message = "User registration completed";
            return response;
        }
        
        response.Status = Status.Failed;
        response.Message = "Something wrong, trying again later";

        return response;
    }

    public async Task<ResponseVm> Login(LoginVm model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);

        var response = new ResponseVm();
        
        if (user is not null)
        {
            var checkCredentials = await _userManager.CheckPasswordAsync(user, model.Password);
            if (checkCredentials)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    response.Status = Status.Success;
                    response.Message = "Login Success";
                    return response;
                }
            }
        }
        
        response.Status = Status.Failed;
        response.Message = "wrong credentials, please try again";
        
        return response;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}