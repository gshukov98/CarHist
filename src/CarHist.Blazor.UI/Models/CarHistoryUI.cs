namespace CarHist.Blazor.UI.Models;

public class CarHistoryUI
{
    public CarHistoryUI() { }

    public CarHistoryUI(string type, DateTimeOffset date, string description)
    {
        Type = type;
        Date = date;
        Description = description;
    }

    public string Type { get; set; }
    public DateTimeOffset Date { get; set; }
    public string Description { get; set; }
}
