using Assignment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ApplicationUser>? ApplicationUser { get; set; }
    public DbSet<Notice>? Notices { get; set; }
    public DbSet<NoticeDetails>? NoticeDetails { get; set; }
    public DbSet<Stats>? Stats { get; set; }
}