using CarHist.Blazor.UI.Services;
using Microsoft.AspNetCore.Components;

namespace CarHist.Blazor.UI.Pages;

public partial class Index : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    protected CarsProvider CarsProvider { get; set; }

    private string SearchVIN;

    public void Insert()
    {
        if (string.IsNullOrEmpty(SearchVIN) == false)
        {
            bool isExisting = CarsProvider.IsExistingCar(SearchVIN);
            if (isExisting)
                NavigationManager.NavigateTo($"/car/details/{SearchVIN}");
        }
    }
}
