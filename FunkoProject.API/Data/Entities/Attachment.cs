namespace FunkoProject.Data.Entities;

public class Attachment
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] Content { get; set; }
    public long Size { get; set; }
    public int UserId { get; set; }
    public virtual User? User { get; set; }
}