using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace CarHist.Blazor.UI.Pages;

public partial class AdminLogin : ComponentBase
{
    private LoginInputModel LoginInputModel = new LoginInputModel();

    public void Login()
    {

    }
}

public class LoginInputModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}
