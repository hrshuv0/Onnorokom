using System.ComponentModel.DataAnnotations;

namespace Assignment.Models;

public class Notice
{
    [Key]
    public int NoticeId { get; set; }

    [Required]
    public string? Title { get; set; }
    
    [Required]
    public string? Description { get; set; }

    public List<NoticeDetails>? Details { get; set; }
}