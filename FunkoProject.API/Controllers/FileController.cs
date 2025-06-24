using Amazon.S3;
using FunkoProject.Data.Entities;
using FunkoProject.Exceptions;
using FunkoProject.Models;
using FunkoProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> UploadFile([FromBody] Attachment file)
    {
        var isSuccess = await _fileService.UploadPhotoToDb(file);
        if (isSuccess)
        {
            return Ok();
        }
        return NotFound();
    }

    [HttpGet("download/{fileName}")]
    public async Task<Attachment> DownloadFile(string fileName)
    {
        try
        {
            var file = await _fileService.DownloadPhotoFromDbAsync("1");
            return file;
        }
        catch (AmazonS3Exception e)
        {
            throw new BadRequestException(e.Message);
        }
    }

    [HttpGet("image/{id}")]
    public async Task<ActionResult<FileDto>> GetImage(int id)
    {
        var attachment = await _fileService.DownloadPhotoFromDbAsync(id.ToString());
        if (attachment == null)
            return NotFound();

        var dto = new FileDto
        {
            FileName = attachment.FileName,
            ContentType = attachment.ContentType,
            Data = Convert.ToBase64String(attachment.Content)
        };
        return Ok(dto);
    }

}