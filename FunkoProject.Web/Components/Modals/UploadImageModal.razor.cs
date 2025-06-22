using System.Net.Http.Json;
using FunkoProject.Web.Models;
using FunkoProject.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace FunkoProject.Web.Components.Modals;

public partial class UploadImageModalBase : ComponentBase
{
    [Inject]
    protected IFileService fileService { get; set; }
    protected bool IsFileSelected { get; set; }
    protected IBrowserFile SelectedFile { get; set; }
    protected IActionResult FileModel = new IActionResult();
    [Parameter] public string UserId { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    

    protected async Task HandleFileSelected(InputFileChangeEventArgs e)
    {
        SelectedFile = e.File;
        if (SelectedFile != null)
        {
            FileModel.FileName = SelectedFile.Name;
            FileModel.ContentType = SelectedFile.ContentType;
            FileModel.Size = SelectedFile.Size;

            using (var stream = new MemoryStream())
            {
                await SelectedFile.OpenReadStream().CopyToAsync(stream);
                FileModel.Content = stream.ToArray();
            }

            IsFileSelected = true;
        }
    }

    protected async Task UploadFile()
    {
        if (!string.IsNullOrEmpty(UserId) && FileModel.Content != null)
        {
            await fileService.UploadFiles(UserId, FileModel);
        }
        await HandleCancel();
    }
    
    protected void RemoveFile()
    {
        FileModel = new IActionResult();
        IsFileSelected = false;
    }
    
    protected async Task HandleCancel()
    {
        if (OnCancel.HasDelegate)
        {
            await OnCancel.InvokeAsync(null);
        }
    }
}