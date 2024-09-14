using FunkoProject.Web.Models;

namespace FunkoProject.Web.Services;

// AccountService.cs
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public interface IAccountService
{
    Task<string> Login(LoginViewModel loginViewModel);
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
}