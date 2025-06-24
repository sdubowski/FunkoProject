using FunkoProject.Data;
using FunkoProject.Data.Entities;

namespace FunkoProject.Repositories;

public interface IFileRepository
{
    Task<Attachment> GetFile(string userId);
    Task<bool> UploadFile(Attachment fileModel);
}

public class FileRepository : IFileRepository
{
    private readonly AppDbContext _appDbContext;

    public FileRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Attachment> GetFile(string userId)
    {
        return _appDbContext.Attachments.FirstOrDefault(f => f.UserId == int.Parse(userId));
    }

    public async Task<bool> UploadFile(Attachment fileModel)
    {
        var currentFile = _appDbContext.Attachments.FirstOrDefault(f => f.UserId == fileModel.UserId);
        if (currentFile != null)
        {
            _appDbContext.Attachments.Remove(currentFile);
        }
        if (fileModel == null)
            return false;

        _appDbContext.Attachments.Add(fileModel);
        _appDbContext.SaveChanges();
        return true;
    }
}