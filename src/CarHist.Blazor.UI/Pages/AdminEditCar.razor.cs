using CarHist.Blazor.UI.Models;
using CarHist.Blazor.UI.Services;
using Elders.Cronus;
using Elders.Cronus.MessageProcessing;
using Microsoft.AspNetCore.Components;
using CarHist.Cars;
using CarHist.Cars.Commands;
using System.ComponentModel.DataAnnotations;

namespace CarHist.Blazor.UI.Pages;

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

    private EditCarInputModel EditCarInputModel;

    protected override Task OnInitializedAsync()
    {
        if (string.IsNullOrEmpty(Id) == false)
        {
            car = CarsProvider.GetCars().Where(x => x.VIN.Equals(Id)).FirstOrDefault();

            EditCarInputModel = new EditCarInputModel(car.Make, car.Model, car.VIN, car.EngineType);
        }

        return base.OnInitializedAsync();
    }

    public void Update()
    {
        if (string.IsNullOrEmpty(EditCarInputModel.VIN))
            return;

        CarId carId = CarId.Parse(FormatCarId(EditCarInputModel.VIN));

        var command = new EditCar(carId, EditCarInputModel.Make, EditCarInputModel.Model, EditCarInputModel.VIN, EditCarInputModel.EngineType);
        Publisher.Publish(command);

        #region Don't look inside
        Thread.Sleep(1000);
        #endregion

        NavigationManager.NavigateTo("/cars");
    }

    private string FormatCarId(string id) => $"urn:{CronusContext.Tenant}:car:{id}";
}

public class EditCarInputModel
{
    public EditCarInputModel()
    {

    }

    public EditCarInputModel(string make, string model, string VIN, string engineType)
    {
        Make = make;
        Model = model;
        this.VIN = VIN;
        EngineType = engineType;
    }

    [Required]
    [MinLength(2), MaxLength(20)]
    public string Make { get; set; }

    [Required]
    [MinLength(2), MaxLength(25)]
    public string Model { get; set; }

    [Required]
    public string VIN { get; set; }

    [Required]
    [MinLength(6), MaxLength(8)]
    public string EngineType { get; set; }
}

