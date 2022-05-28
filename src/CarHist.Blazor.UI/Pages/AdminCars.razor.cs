using CarHist.Blazor.UI.Models;
using CarHist.Blazor.UI.Services;
using Elders.Cronus;
using Elders.Cronus.MessageProcessing;
using Elders.Cronus.Projections;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace CarHist.Blazor.UI.Pages;

public partial class AdminCars : ComponentBase
{
    private HubConnection hubConnection;

    [Inject]
    protected IProjectionReader Projections { get; set; }

    [Inject]
    protected CarsProvider CarsProvider { get; set; }

    [Inject]
    protected CronusContext CronusContext { get; set; }

    [Inject]
    protected IPublisher<ICommand> Publisher { get; set; }

    protected List<CarStateUI> cars = new List<CarStateUI>();

    protected override async Task OnInitializedAsync()
    {
        cars = CarsProvider.GetCars().ToList();
        hubConnection = new HubConnectionBuilder()
            .WithUrl("http://127.0.0.1:17677" + "/hub/cars")
            .WithAutomaticReconnect()
            .Build();

        hubConnection.On<string, string>("CarCreation", (carId, name) =>
          {
              if (cars.Any(x => x.VIN.Equals(carId) == false))
                  cars.Add(new CarStateUI(carId, name));

              StateHasChanged();
          });

        await hubConnection.StartAsync();
    }

    public void Dispose()
    {
        if (hubConnection is not null)
            hubConnection.DisposeAsync();
    }

    //TODO: Implement delete
    public void Delete(CarStateUI car)
    {

    }
}
