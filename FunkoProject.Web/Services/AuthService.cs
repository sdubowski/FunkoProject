using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace FunkoProject.Web.Services;

public interface IAuthService
{ 
    Task Logout();
}
public class AuthService:IAuthService
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;
    private ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public AuthService(ILocalStorageService localStorage, HttpClient httpClient, NavigationManager navigationManager)
    {
        _localStorage = localStorage;
        _httpClient = httpClient;
        _navigationManager = navigationManager;
    }
    
    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("accessToken");
        _navigationManager.NavigateTo("/login", true);
    }
}