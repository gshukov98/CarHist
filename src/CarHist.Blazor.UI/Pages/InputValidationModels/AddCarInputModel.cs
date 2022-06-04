using System.ComponentModel.DataAnnotations;

namespace CarHist.Blazor.UI.Pages.InputValidationModels;

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
