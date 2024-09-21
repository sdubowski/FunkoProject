using System.Net.Http.Json;
using FunkoProject.Web.Enums;
using FunkoProject.Web.Models;
using FunkoProject.Web.Services;
using Microsoft.AspNetCore.Components;

namespace FunkoProject.Web.Pages;

public partial class ProfilePage : ComponentBase
{
    private User user = new User();
    private bool isEditModalOpen = false;
    private bool isUploadModalOpen = false;
    [Inject]
    private IUserService UserService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        user = await UserService.GetUserAsync();
    }

    private void OpenEditModal(ModalTypeEnum modalTypeEnum)
    {
        switch (modalTypeEnum)
        {
            case ModalTypeEnum.Edit:
                isEditModalOpen = true;
                break;
            case ModalTypeEnum.Upload:
                isUploadModalOpen = true;
                break;
        }
    }

    private void CloseEditModal(ModalTypeEnum modalTypeEnum)
    {
        switch (modalTypeEnum)
        {
            case ModalTypeEnum.Edit:
                isEditModalOpen = false;
                break;
            case ModalTypeEnum.Upload:
                isUploadModalOpen = false;
                break;
        }
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