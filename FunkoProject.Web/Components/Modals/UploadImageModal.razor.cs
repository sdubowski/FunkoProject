using System.Net.Http.Json;
using FunkoProject.Web.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FunkoProject.Web.Components.Modals;

public partial class UploadImageModalBase : ComponentBase
{
    [Inject]
    protected HttpClient Http { get; set; }
    protected bool IsFileSelected { get; set; }
    protected IBrowserFile SelectedFile { get; set; }
    protected string UploadMessage { get; set; }
    protected FileModel FileModel = new FileModel();
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
            FileModel.UserId = UserId;
            var response = await Http.PostAsJsonAsync("api/files/UploadFile", FileModel);

            if (response.IsSuccessStatusCode)
            {
                UploadMessage = "File uploaded successfully";
            }
            else
            {
                UploadMessage = "File upload failed";
            }
        }

        await HandleCancel();
    }
    
    protected void RemoveFile()
    {
        FileModel = new FileModel();
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