using Microsoft.AspNetCore.Identity;

namespace Assignment.Models;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public List<NoticeDetails>? NoticeDetails { get; set; }
}