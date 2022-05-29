using CarHist.Blazor.UI.Models;
using CarHist.Blazor.UI.Services;
using Microsoft.AspNetCore.Components;

namespace CarHist.Blazor.UI.Pages;

public partial class AppendHistory : ComponentBase
{
    [Inject]
    protected CarsProvider CarsProvider { get; set; }

    private string SearchVIN;

    protected List<CarStateUI> cars = new List<CarStateUI>();

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

    //TODO: Implement this
    public void Update(CarStateUI car)
    {
    }
}
