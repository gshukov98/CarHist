using CarHist.Blazor.UI.Models;
using CarHist.Blazor.UI.Services;
using Elders.Cronus;
using Elders.Cronus.MessageProcessing;
using Microsoft.AspNetCore.Components;
using CarHist.Cars;
using CarHist.Cars.Commands;

namespace CarHist.Blazor.UI.Pages
{
    public partial class AdminEditCar : ComponentBase
    {
        public CarStateUI car = new CarStateUI();

        // This is Car VIN
        [Parameter]
        public string Id { get; set; }

        [Inject]
        protected CarsProvider CarsProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected CronusContext CronusContext { get; set; }

        [Inject]
        protected IPublisher<ICommand> Publisher { get; set; }

        protected override Task OnInitializedAsync()
        {
            if (string.IsNullOrEmpty(Id) == false)
            {
                car = CarsProvider.GetCars().Where(x => x.VIN.Equals(Id)).FirstOrDefault();
            }

            return base.OnInitializedAsync();
        }

        public void Update()
        {
            if (string.IsNullOrEmpty(car.VIN))
                return;

            var carId = CarId.Parse(FormatCarId(car.VIN));

            var command = new EditCar(carId, car.Make, car.Model, car.VIN, car.EngineType);
            Publisher.Publish(command);

            NavigationManager.NavigateTo("/cars");
        }

        private string FormatCarId(string id) => $"urn:{CronusContext.Tenant}:car:{id}";
    }
}
