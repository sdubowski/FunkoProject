using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace FunkoProject.Web.Services;

public interface IAuthService
{ 
    Task Logout();
    Task<string> GetIdFromToken();
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
    
    public async Task<string> GetIdFromToken()
    {
        var token = await _localStorage.GetItemAsync<string>("accessToken");
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        if (jsonToken == null)
            throw new Exception("Token is not valid");

        return jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
    }
}