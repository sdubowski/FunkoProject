using Amazon.S3;
using Amazon.S3.Model;
using FunkoProject.Data.Entities;
using FunkoProject.Repositories;

namespace FunkoProject.Services;

public interface IFileService
{
    Task<(bool IsSuccess, string Message)> UploadFileAsync(FileModel file);
    Task<FileModel> DownloadFileAsync(string fileName, string id);
}

public class FileService : IFileService
{
    private readonly IAmazonS3 _s3Client;
    private readonly IFileRepository _fileRepository;
    private const string BucketName = "funko-project-bucket";

    public FileService(IAmazonS3 s3Client, IFileRepository fileRepository)
    {
        _s3Client = s3Client;
        _fileRepository = fileRepository;
    }

    public async Task<(bool IsSuccess, string Message)> UploadFileAsync(FileModel file)
    {
        if (file == null || file.Content == null || file.Content.Length == 0)
        {
            return (false, "Brak pliku lub plik jest pusty.");
        }

        try
        {
            using var stream = new MemoryStream(file.Content);
            var putRequest = new PutObjectRequest
            {
                BucketName = BucketName,
                Key = file.FileName,
                InputStream = stream,
                ContentType = file.ContentType
            };

            var response = await _s3Client.PutObjectAsync(putRequest);
            var fileToUpload = new FileModel()
            {
                FileName = file.FileName,
                Content = stream.ToArray(),
                ContentType = file.ContentType,
                Size = stream.Length,
                UserId = "1"
            };
            _fileRepository.UploadFile(fileToUpload);
            return (true, $"Plik {file.FileName} został pomyślnie przesłany do S3.");
        }
        catch (Exception ex)
        {
            return (false, $"Wystąpił błąd przy przesyłaniu pliku: {ex.Message}");
        }
    }

    public async Task<FileModel> DownloadFileAsync(string fileName, string id)
    {
        try
        {
            var request = new GetObjectRequest
            {
                BucketName = BucketName,
                Key = fileName
            };

            using var response = await _s3Client.GetObjectAsync(request);
            using var responseStream = response.ResponseStream;
            using var memoryStream = new MemoryStream();
            await responseStream.CopyToAsync(memoryStream);
        
            return new FileModel
            {
                FileName = fileName,
                Content = memoryStream.ToArray(),
                ContentType = response.Headers.ContentType,
                Size = response.Headers.ContentLength,
                UserId = id
            };
        }
        catch (AmazonS3Exception)
        {
            throw new FileNotFoundException($"File {fileName} was not found.");
        }
    }
}