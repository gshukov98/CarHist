using System.ComponentModel.DataAnnotations;
using CarHist.Blazor.UI.Models;
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

public class AddCarInputModel
{
    [Required]
    [MinLength(2), MaxLength(20)]
    public string Make { get; set; }

    [Required]
    [MinLength(2), MaxLength(25)]
    public string Model { get; set; }

    [Required]
    [MinLength(10), MaxLength(20)]
    public string VIN { get; set; }

    [Required]
    [MinLength(6), MaxLength(8)]
    public string EngineType { get; set; }
}
