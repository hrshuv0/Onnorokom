namespace Assignment.Models;

public class Stats
{
    public int Id { get; set; }
    
    public List<Notice>? Notice { get; set; }
    public List<NoticeDetails>? Details { get; set; }
    public List<ApplicationUser>? ApplicationUser { get; set; }
}