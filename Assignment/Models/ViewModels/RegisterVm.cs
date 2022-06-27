using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.ViewModels;

public class RegisterVm
{
    [Required(ErrorMessage = "user name is required")]
    [Display(Name = "User Name")]
    public string? UserName { get; set; }
    
    [Required(ErrorMessage = "password is required")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "password didn't password")]
    public string? ConfirmPassword { get; set; }
}