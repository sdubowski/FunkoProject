using FunkoProject.Web.Models.ViewModels;
using FunkoProject.Web.Services;
using Microsoft.AspNetCore.Components;

namespace FunkoProject.Web.Pages
{
    public partial class RegisterPage
    {
        private RegisterViewModel model = new();
        private bool _isRegistered = false;
        private bool showToast = false;

        [Inject]
        private IAccountService _accountService {  get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }
        
        private async Task HandleRegistration()
        {
            model.RoleId = 1;
            string responseMessage = await _accountService.Register(model);
            if (responseMessage == "success")
            {
                _isRegistered = true;
            }

            if (_isRegistered)
            {
                showToast = true;
                StateHasChanged();
                
                await Task.Delay(3000);
                showToast = false;
                StateHasChanged();
            }

            NavigationManager.NavigateTo("/");
        }
    }
}
