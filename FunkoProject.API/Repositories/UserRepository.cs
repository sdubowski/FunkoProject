using FunkoProject.Data;
using FunkoProject.Data.Entities;
using Microsoft.EntityFrameworkCore;

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
        return _appDbContext.Users
            .Include(u => u.Friends)
            .ThenInclude(uf => uf.Friend)
            .First(u => u.Id == userId);
    }
}