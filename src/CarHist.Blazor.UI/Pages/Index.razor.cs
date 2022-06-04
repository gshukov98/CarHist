using CarHist.Blazor.UI.Pages.InputValidationModels;
using CarHist.Blazor.UI.Services;
using Microsoft.AspNetCore.Components;

namespace CarHist.Blazor.UI.Pages;

public partial class Index : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    protected CarsProvider CarsProvider { get; set; }

    private SearchVINInputModel SearchVINInputModel = new SearchVINInputModel();

    public void Insert()
    {
        if (string.IsNullOrEmpty(SearchVINInputModel.SearchVIN) == false)
        {
            bool isExisting = CarsProvider.IsExistingCar(SearchVINInputModel.SearchVIN);
            if (isExisting)
                NavigationManager.NavigateTo($"/car/details/{SearchVINInputModel.SearchVIN}");
        }
    }
}
