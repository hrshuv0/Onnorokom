namespace Assignment.Models.ViewModels;

public class ResponseVm
{
    public Status Status { get; set; }
    public string? Message { get; set; }
}

public enum Status
{
    Success,
    Failed
}