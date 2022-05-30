namespace CarHist.Blazor.UI.Models;

public class DataItem
{
    public DataItem(long mileage, DateTime date)
    {
        Mileage = mileage;
        Date = date;
    }
    public DateTime Date { get; set; }
    public long Mileage { get; set; }
}
