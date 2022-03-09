using CarHist.Cars;
using CarHist.UI.Models;
using Elders.Cronus;
using Elders.Cronus.MessageProcessing;
using Microsoft.AspNetCore.Components;

namespace CarHist.UI.Pages
{
    public partial class AddCar : ComponentBase
    {
        public CarStateUI car = new CarStateUI();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected CronusContext CronusContext { get; set; }

        [Inject]
        protected IPublisher<ICommand> Publisher { get; set; }

        public void Insert()
        {
            CarId id = new CarId(vin, CronusContext.Tenant);

            //create car command

            NavigationManager.NavigateTo("/cars");
        }
    }
}
