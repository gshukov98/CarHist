using CarHist.Blazor.UI.Models;
using CarHist.Blazor.UI.Pages.InputValidationModels;
using CarHist.Cars;
using CarHist.Cars.Commands;
using Elders.Cronus;
using Elders.Cronus.MessageProcessing;
using Microsoft.AspNetCore.Components;

namespace CarHist.Blazor.UI.Pages;

public partial class AddCar : ComponentBase
{
    public CarStateUI car = new CarStateUI();

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    protected CronusContext CronusContext { get; set; }

    [Inject]
    protected IPublisher<ICommand> Publisher { get; set; }

    private AddCarInputModel AddCarInputModel = new AddCarInputModel();

    public void Insert()
    {
        CarId id = new CarId(AddCarInputModel.VIN, CronusContext.Tenant);

        var command = new CreateCar(id, AddCarInputModel.Make, AddCarInputModel.Model, AddCarInputModel.VIN, AddCarInputModel.EngineType);

        Publisher.Publish(command);

        NavigationManager.NavigateTo("/cars");
    }
}
