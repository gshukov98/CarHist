using System.ComponentModel.DataAnnotations;

namespace CarHist.Blazor.UI.Pages.InputValidationModels;

public class AppendHistoryInputModel
{
    [Required]
    public string Type { get; set; }

    [Required]
    [MinLength(1), MaxLength(1000)]
    public string Description { get; set; }
}
