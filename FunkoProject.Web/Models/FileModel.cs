namespace FunkoProject.Web.Models;

public class FileModel
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] Content { get; set; }
    public long Size { get; set; }
    public string UserId { get; set; }
}