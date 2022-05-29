using CarHist.Blazor.UI.Models;
using CarHist.Blazor.UI.Services;
using CarHist.Cars;
using CarHist.Cars.Commands;
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

    [Inject]
    public NavigationManager NavigationManager { get; set; }

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

        hubConnection.On<string, string>("CarEdit", (carId, name) =>
         {
             if (cars.Any(x => x.VIN.Equals(carId)))
             {
                 CarStateUI oldCar = cars.Where(x => x.VIN.Equals(carId)).FirstOrDefault();
                 cars.Remove(oldCar);
                 cars.Add(new CarStateUI(carId, name));
             }

             StateHasChanged();
         });

        hubConnection.On<string, string>("CarDeleted", (carId, name) =>
        {
            if (cars.Any(x => x.VIN.Equals(carId)))
            {
                CarStateUI deletedCar = cars.Where(x => x.VIN.Equals(carId)).FirstOrDefault();
                cars.Remove(deletedCar);
            }

            StateHasChanged();
        });

        await hubConnection.StartAsync();
    }

    public void Dispose()
    {
        if (hubConnection is not null)
            hubConnection.DisposeAsync();
    }

    public void Delete(CarStateUI car)
    {
        if (string.IsNullOrEmpty(car.VIN))
            return;

        CarId carId = CarId.Parse(FormatCarId(car.VIN));

        var command = new DeleteCar(carId);
        Publisher.Publish(command);

        #region Don't look inside
        Thread.Sleep(1000);
        #endregion

        NavigationManager.NavigateTo("/cars");
    }

    private string FormatCarId(string id) => $"urn:{CronusContext.Tenant}:car:{id}";
}
