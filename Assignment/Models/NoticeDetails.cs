using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Assignment.Models;

public class NoticeDetails
{
    public int Id { get; set; }
    
    [ForeignKey("Notice")]
    public int NoticeId { get; set; }
    public Notice? Notice { get; set; }

    [ForeignKey("ApplicationUser")]
    public string? UserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }

    public int HitCount { get; set; }
    
}