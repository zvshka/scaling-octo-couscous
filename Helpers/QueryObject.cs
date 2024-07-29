namespace WebApp1.Helpers;

public class QueryObject
{
    public string? Search { get; set; } = null;
    public DateTime Date { get; set; } = DateTime.Today;
    public bool IsDescending { get; set; } = false;
}