using System.ComponentModel.DataAnnotations;

namespace CarHist.Blazor.UI.Pages.InputValidationModels;

public class SearchVINInputModel
{
    [Required]
    [MinLength(10)]
    public string SearchVIN { get; set; }
}
