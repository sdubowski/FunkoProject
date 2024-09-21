using FunkoProject.Data;
using FunkoProject.Data.Entities;

namespace FunkoProject.Repositories;

public interface IFileRepository
{
    FileModel GetFile(string userId);
    void UploadFile(FileModel fileModel);
}

public class FileRepository : IFileRepository
{
    private readonly AppDbContext _appDbContext;

    public FileRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public FileModel GetFile(string userId)
    {
        return _appDbContext.Files.FirstOrDefault(f => f.UserId == userId) ?? throw new FileNotFoundException("not found");
    }

    public void UploadFile(FileModel fileModel)
    {
        _appDbContext.Files.Add(fileModel);
    }
}