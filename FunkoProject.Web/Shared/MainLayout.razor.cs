using Blazored.LocalStorage;
using FunkoProject.Web.Services;
using Microsoft.AspNetCore.Components;

namespace FunkoProject.Web.Shared
{
    public class MainLayoutBase : LayoutComponentBase
    {
        public bool isUserLoggedIn = false;
        [Inject] private IAuthService _authService { get; set; }
        [Inject] private ILocalStorageService _localStorage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("accessToken");
            if (token == null)
            {
                isUserLoggedIn = false;
            }
            else
            {
                isUserLoggedIn = true;
            }
        }

        public async Task Logout()
        {
            await _authService.Logout();
        }
    }
}
