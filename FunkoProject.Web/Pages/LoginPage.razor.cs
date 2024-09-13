using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace FunkoProject.Web.Pages;

public class LoginPageBase : ComponentBase
{
    protected LoginModel LoginModel { get; set; } = new LoginModel();

    protected void HandleValidSubmit()
    {
        // Tutaj dodaj logikę logowania
        Console.WriteLine($"Próba logowania: {LoginModel.Username}");
    }
}

public class LoginModel
{
    [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Hasło jest wymagane")]
    public string Password { get; set; }
}