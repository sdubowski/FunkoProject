using Amazon.S3;
using FunkoProject.Data.Entities;
using FunkoProject.Exceptions;
using FunkoProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FunkoProject.Controllers;

[ApiController]
[Route("api/files")]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;
    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    [HttpPost]
    [Route("UploadFile")]
    public async Task<IActionResult> UploadFile([FromBody]FileModel file)
    {
        var (isSuccess, message) = await _fileService.UploadFileAsync(file);
        if (isSuccess)
        {
            return Ok(new { Message = message });
        }
        return BadRequest(message);
    }

    [HttpGet("download/{fileName}")]
    public async Task<FileModel> DownloadFile(string fileName)
    {
        try
        {
            var file = await _fileService.DownloadFileAsync(fileName, "1");
            return file;
        }
        catch (AmazonS3Exception e)
        {
            throw new BadRequestException(e.Message);
        }
    }
    
}