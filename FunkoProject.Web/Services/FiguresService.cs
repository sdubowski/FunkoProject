using FunkoProject.Web.Components;
using FunkoProject.Web.Models;
using FunkoProject.Web.Models.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace FunkoProject.Web.Services
{
    public interface IFiguresService
    {
        Task<List<Figure>> GetUserFiguresAsync(int userId);
        Task<bool> RegisterFigureForUser(UserFigureViewModel userFigureViewModel);
    }

    public class FiguresService : IFiguresService
    {
        private HttpClient _httpClient { get; set; }

        public FiguresService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Figure>> GetUserFiguresAsync(int userId)
        {
            var figures = await _httpClient.GetFromJsonAsync<List<Figure>>($"api/Figure/GetUserFigures/{userId}");
            if (figures == null)
            {
                return new List<Figure>();
            }
            else
            {
                return figures;
            }
        }
        public async Task<bool> RegisterFigureForUser(UserFigureViewModel userFigureViewModel)
        {
            var response = await _httpClient.PostAsJsonAsync<UserFigureViewModel>($"api/Figure/RegisterFigureForUser", userFigureViewModel);
            return response.IsSuccessStatusCode;
        }
    }
}
