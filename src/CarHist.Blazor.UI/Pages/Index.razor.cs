using System.ComponentModel.DataAnnotations;
using CarHist.Blazor.UI.Services;
using Microsoft.AspNetCore.Components;

namespace CarHist.Blazor.UI.Pages;

public partial class Index : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    protected CarsProvider CarsProvider { get; set; }

    private SearchVINInputModel searchVINInputModel = new SearchVINInputModel();

    public void Insert()
    {
        if (string.IsNullOrEmpty(searchVINInputModel.SearchVIN) == false)
        {
            bool isExisting = CarsProvider.IsExistingCar(searchVINInputModel.SearchVIN);
            if (isExisting)
                NavigationManager.NavigateTo($"/car/details/{searchVINInputModel.SearchVIN}");
        }
    }
}

public class SearchVINInputModel
{
    [Required]
    [MinLength(10)]
    public string SearchVIN { get; set; }
}
