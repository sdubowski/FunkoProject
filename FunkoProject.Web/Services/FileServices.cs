using System.Net;
using System.Net.Http.Json;
using System.Net.Mail;
using FunkoProject.Web.Models;
using static System.Net.WebRequestMethods;

namespace FunkoProject.Web.Services;


public interface IFileService
{
    Task<string> UploadFiles(string userId, FileModel fileModel);
    Task<FileDto> GetFile(int id);
}


public class FileServices : IFileService
{
    private HttpClient _httpClient;

    public FileServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<string> UploadFiles(string userId, FileModel fileModel)
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

    public async Task<FileDto> GetFile(int id)
    {
        try
    {
        var response = await _httpClient.GetAsync($"api/files/image/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null; // brak zdjęcia — zwracamy null
        }
        
        response.EnsureSuccessStatusCode(); // inne błędy nadal rzucą wyjątek

        var fileDto = await response.Content.ReadFromJsonAsync<FileDto>();
        return fileDto;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Błąd pobierania pliku: {ex.Message}");
        return null; // w razie innego błędu też zwracamy null lub możesz przepuścić wyjątek dalej
    }
    }
}