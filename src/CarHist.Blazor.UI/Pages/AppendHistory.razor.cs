using System.ComponentModel.DataAnnotations;
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

    private SearchVINInputModel SearchVINInputModel = new SearchVINInputModel();

    private AppendHistoryInputModel AppendHistoryInputModel = new AppendHistoryInputModel();

    private string EditingVIN = "VIN: Empty";

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
        if (string.IsNullOrEmpty(SearchVINInputModel.SearchVIN.Trim()) == false)
            cars = CarsProvider.GetCarsBySearchVIN(SearchVINInputModel.SearchVIN).ToList();
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
        var command = new Cars.Commands.AppendHistory(Id, AppendHistoryInputModel.Type, AppendHistoryInputModel.Description, "Test Company");

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

public class AppendHistoryInputModel
{
    [Required]
    public string Type { get; set; }

    [Required]
    [MinLength(1), MaxLength(1000)]
    public string Description { get; set; }
}
