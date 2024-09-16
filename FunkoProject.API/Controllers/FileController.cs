using Amazon.S3;
using Amazon.S3.Model;
using FunkoProject.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FunkoProject.Controllers;

[ApiController]
[Route("api/files")]
public class FileController : ControllerBase
{
    private readonly IAmazonS3 _s3Client;
    public FileController(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }
    
    [HttpPost]
    [Route("UploadFile")]
    public async Task<IActionResult> UploadFile([FromBody]FileModel file)
    {
        if (file == null || file.Content == null || file.Content.Length == 0)
        {
            return BadRequest("Brak pliku lub plik jest pusty.");
        }

        var bucketName = "funko-project-bucket";
        var key = file.FileName;

        try
        {
            using (var stream = new MemoryStream(file.Content))
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    InputStream = stream,
                    ContentType = file.ContentType
                };

                var response = await _s3Client.PutObjectAsync(putRequest);
                return Ok(new { Message = "Plik został pomyślnie przesłany do S3.", FileName = key });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Wystąpił błąd przy przesyłaniu pliku: {ex.Message}");
        }
    }
}