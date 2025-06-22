using System.Net.Http.Json;
using FunkoProject.Web.Components;
using FunkoProject.Web.Enums;
using FunkoProject.Web.Models;
using FunkoProject.Web.Services;
using Microsoft.AspNetCore.Components;

namespace FunkoProject.Web.Pages;

public partial class ProfilePage : ComponentBase
{
    private User user = new User();
    private List<Figure> userFigure = new List<Figure>();
    private int userFigures;
    private bool isEditModalOpen = false;
    private bool isUploadModalOpen = false;
    [Inject]
    private IUserService UserService { get; set; }
    [Inject]
    private IFileService FileService { get; set; }
    [Inject]
    private IFiguresService FiguresService { get; set; }
    

    protected override async Task OnInitializedAsync()
    {
        user = await UserService.GetUserAsync();
        userFigure = await FiguresService.GetUserFiguresAsync(user.Id);
        userFigures = userFigure.Count;
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