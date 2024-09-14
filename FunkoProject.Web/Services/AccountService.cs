using FunkoProject.Web.Models;

namespace FunkoProject.Web.Services;

using FunkoProject.Web.Models.ViewModels;

// AccountService.cs
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public interface IAccountService
{
    Task<string> Login(LoginViewModel loginViewModel);
    Task<string> Register(RegisterViewModel user);
}

public class AccountService:IAccountService
{
    private readonly HttpClient _httpClient;

    public AccountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> Login(LoginViewModel loginViewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Account/login", loginViewModel);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> Register(RegisterViewModel user)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Account/register", user);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}