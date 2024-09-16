using Amazon;
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;

namespace FunkoProject.Controllers;

[ApiController]
[Route("api/buckets")]
public class BucketController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IAmazonS3 _s3Client;

    public BucketController(IConfiguration configuration, IAmazonS3 s3Client)
    {
        _configuration = configuration;
        _s3Client = s3Client;
    }
    
    [HttpGet]
    [Route("GetBucketsList")]
    public async Task<IActionResult> ListAsync()
    {
        var data = await _s3Client.ListBucketsAsync();
        var buckets = data.Buckets.Select(b => { return b.BucketName; });
        return Ok(buckets);
    }
}