using System.Net;
using System.Net.Http.Json;
using FunkoProject.Web.Models;

namespace FunkoProject.Web.Services;


public interface IFileService
{
    Task<string> UploadFiles(string userId, IActionResult fileModel);
    Task GetFile(string userId);
}


public class FileServices : IFileService
{
    private HttpClient _httpClient;

    public FileServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<string> UploadFiles(string userId, IActionResult fileModel)
    {
        fileModel.UserId = userId;
        string uploadMessage;
        var response = await _httpClient.PostAsJsonAsync("api/files/UploadFile", fileModel);

        if (response.IsSuccessStatusCode)
        {
            uploadMessage = "File uploaded successfully";
        }
        else
        {
            uploadMessage = "File upload failed";
        }
        return uploadMessage;
    }

    public async Task GetFile(string userId)
    {
/*        var response = await _httpClient.GetAsync($"api/files/{userId}");
        return response;*/
    }
}