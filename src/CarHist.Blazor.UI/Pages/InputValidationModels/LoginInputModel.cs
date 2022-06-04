using System.ComponentModel.DataAnnotations;

namespace CarHist.Blazor.UI.Pages.InputValidationModels;

public class LoginInputModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}
