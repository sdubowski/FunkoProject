using FunkoProject.Web.Models;

namespace FunkoProject.Web.Services;

// AccountService.cs
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class AccountService
{
    private readonly HttpClient _httpClient;

    public AccountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> Login(User user)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Account/login", user);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}