using Amazon.S3;
using Amazon.S3.Model;
using FunkoProject.Data.Entities;
using FunkoProject.Repositories;

namespace FunkoProject.Services;

public interface IFileService
{
    Task<bool> UploadPhotoToDb(Attachment file);
    Task<Attachment> DownloadPhotoFromDbAsync(string userId);
}

public class FileService : IFileService
{
    private readonly IFileRepository _fileRepository;

    public FileService(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task<bool> UploadPhotoToDb(Attachment file)
    {
       var result = await _fileRepository.UploadFile(file);
        return result;
    }

    public async Task<Attachment> DownloadPhotoFromDbAsync(string userId)
    {
        var result = await _fileRepository.GetFile(userId);
        return result;
    }
}