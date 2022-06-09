using CarHist.Blazor.UI.Models;
using CarHist.Blazor.UI.Pages.InputValidationModels;
using CarHist.Blazor.UI.Services;
using CarHist.Cars;
using Elders.Cronus;
using Elders.Cronus.MessageProcessing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace CarHist.Blazor.UI.Pages;

public partial class AppendHistory : ComponentBase
{
    private HubConnection hubConnection;

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

    protected override async Task OnInitializedAsync()
    {
        cars = CarsProvider.GetCars().ToList();
        hubConnection = new HubConnectionBuilder()
            .WithUrl("http://127.0.0.1:17677" + "/hub/cars")
            .WithAutomaticReconnect()
            .Build();

        hubConnection.On<string, string>("CarHistoryAppended", (carId, name) =>
        {
            history = CarHistoryProvider.GetCarByVIN(CarId.Parse(carId)).ToList();

            StateHasChanged();
        });

        await hubConnection.StartAsync();
    }

    public void Dispose()
    {
        if (hubConnection is not null)
            hubConnection.DisposeAsync();
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
        var command = new Cars.Commands.AppendHistory(Id, AppendHistoryInputModel.Type, AppendHistoryInputModel.Description, "Test Company");

        Publisher.Publish(command);

        // Eventual consistency
        history.Add(new CarHistoryUI(AppendHistoryInputModel.Type, DateTimeOffset.UtcNow, AppendHistoryInputModel.Description));
    }

    private string FormatCarId(string id) => $"urn:{CronusContext.Tenant}:car:{id}";

    private string[] typeItems = new[]
            {
                "Repair",
                "Service",
                "Mileage"
            };
}
