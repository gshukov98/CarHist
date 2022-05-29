using CarHist.Blazor.UI.Models;
using CarHist.Blazor.UI.Services;
using CarHist.Cars;
using Elders.Cronus;
using Elders.Cronus.MessageProcessing;
using Microsoft.AspNetCore.Components;

namespace CarHist.Blazor.UI.Pages;

public partial class AppendHistory : ComponentBase
{
    [Inject]
    protected CarsProvider CarsProvider { get; set; }

    [Inject]
    protected CarHistoryProvider CarHistoryProvider { get; set; }

    [Inject]
    protected CronusContext CronusContext { get; set; }

    [Inject]
    protected IPublisher<ICommand> Publisher { get; set; }

    private CarId Id;

    private string SearchVIN;

    private string EditingVIN = "VIN: Empty";

    string type;

    string description;

    protected List<CarStateUI> cars = new List<CarStateUI>();

    protected List<CarHistoryUI> history = new List<CarHistoryUI>();

    //TODO: Add sigralR here
    protected override Task OnInitializedAsync()
    {
        cars = CarsProvider.GetCars().ToList();

        return base.OnInitializedAsync();
    }

    public void Search()
    {
        if (string.IsNullOrEmpty(SearchVIN.Trim()) == false)
            cars = CarsProvider.GetCarsBySearchVIN(SearchVIN).ToList();
        else
            cars = CarsProvider.GetCars().ToList();

        StateHasChanged();
    }

    public void Update(CarStateUI car)
    {
        EditingVIN = $"VIN: {car.VIN}";

        Id = CarId.Parse(FormatCarId(car.VIN));
        history = CarHistoryProvider.GetCarByVIN(Id).ToList();
    }

    public void Insert()
    {
        //validate props
        var command = new Cars.Commands.AppendHistory(Id, type, description, "Test Company");

        Publisher.Publish(command);
    }

    private string FormatCarId(string id) => $"urn:{CronusContext.Tenant}:car:{id}";

    private string[] typeItems = new[]
            {
                "Repair",
                "Service",
                "Mileage"
            };
}
