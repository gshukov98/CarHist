using System.ComponentModel.DataAnnotations;

namespace CarHist.Blazor.UI.Pages.InputValidationModels;

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

