using Microsoft.AspNetCore.Components;
using FunkoProject.Web.Models;
using FunkoProject.Web.Services;
using Blazored.LocalStorage;

namespace FunkoProject.Web.Pages;
public class LoginPageBase : ComponentBase
{
    [Inject] private IAccountService _accountService { get; set; }
    [Inject] private NavigationManager _navigationManager { get; set; }
    [Inject] private ILocalStorageService _localStorage { get; set; }

    protected LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();
    protected string ErrorMessage { get; set; }

    protected async Task HandleValidSubmit()
    {
        try
        {
            var token = await _accountService.Login(LoginViewModel);
        
            await _localStorage.SetItemAsync("accessToken", token);
            _navigationManager.NavigateTo("/", true);
        }
        catch (HttpRequestException e)
        {
            ErrorMessage = "Błąd logowania. Sprawdź swoje dane i spróbuj ponownie.";
            Console.WriteLine($"Błąd logowania: {e.Message}");
        }
    }
}