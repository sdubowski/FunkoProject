using System.Net.Http.Json;
using FunkoProject.Web.Models;
using FunkoProject.Web.Services;
using Microsoft.AspNetCore.Components;

namespace FunkoProject.Web.Pages;

public partial class ProfilePage : ComponentBase
{
    private User user = new User();
    private bool isEditModalOpen = false;
    [Inject]
    private IUserService UserService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        user = await UserService.GetUserAsync();
    }

    private void OpenEditModal()
    {
        isEditModalOpen = true;
    }

    private void CloseEditModal()
    {
        isEditModalOpen = false;
    }

    private async Task SaveChanges()
    {
        /*// Wyślij dane użytkownika do API, aby zapisać zmiany (zastąp swoim endpointem)
        //var response = await Http.PutAsJsonAsync("api/user/1", user);
        if (response.IsSuccessStatusCode)
        {
            CloseEditModal();
        }*/
    }
}