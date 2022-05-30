using System.Globalization;
using CarHist.Blazor.UI.Models;
using CarHist.Blazor.UI.Services;
using CarHist.Cars;
using Elders.Cronus.MessageProcessing;
using Microsoft.AspNetCore.Components;

namespace CarHist.Blazor.UI.Pages;

public partial class CarDetails : ComponentBase
{
    [Parameter]
    public string Id { get; set; }

    [Inject]
    protected CarHistoryProvider CarHistoryProvider { get; set; }

    [Inject]
    protected CarsProvider CarsProvider { get; set; }

    [Inject]
    protected CronusContext CronusContext { get; set; }

    private string ShowingVIN = "VIN: Empty";

    protected List<DataItem> CarHistory = new List<DataItem>();

    protected List<CarHistoryUI> History = new List<CarHistoryUI>();

    protected List<CarStateUI> CarInfo = new List<CarStateUI>();

    protected override Task OnInitializedAsync()
    {
        ShowingVIN = $"VIN: {Id}";

        CarId carId = CarId.Parse(FormatCarId(Id));
        History = CarHistoryProvider.GetCarWithoutMileageByVIN(carId).ToList();
        CarHistory = CarHistoryProvider.GetCarMileageByVIN(carId).Select(x => new DataItem(long.Parse(x.Description), x.Date.DateTime)).ToList();

        CarStateUI car = CarsProvider.GetCarInfo(carId);
        if (car is not null)
            CarInfo.Add(car);

        return base.OnInitializedAsync();
    }

    private string FormatCarId(string id) => $"urn:{CronusContext.Tenant}:car:{id}";

    static string FormatAsKM(object value)
    {
        return $"{(double)value} km";
    }
}
