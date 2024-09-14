using FunkoProject.Data;
using FunkoProject.Data.Entities;

namespace FunkoProject.Repositories;

public interface IUserRepository
{
    User Get(int userId);   
}
public class UserRepository:IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public User Get(int userId)
    {
        var user = _appDbContext.Users.FirstOrDefault(u => u.Id == userId);
        return user;
    }
}