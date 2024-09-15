using System.Net.Http.Json;
using FunkoProject.Web.Models;

namespace FunkoProject.Web.Services;

public interface IUserService
{
    Task<User> GetUserAsync();
}

public class UserService : IUserService
{
    private IAuthService _authService { get; set; }
    private HttpClient _httpClient { get; set; }

    public UserService(HttpClient httpClient, IAuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<User> GetUserAsync()
    {
        var userId = await _authService.GetIdFromToken();
        var user = await _httpClient.GetFromJsonAsync<User>($"api/User/GetUserById/{userId}");
        if (user == null)
        {
            throw new ApplicationException("User not found");
        }
        return user;
    }
}