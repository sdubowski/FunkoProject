using FunkoProject.Common.Atributes;
using FunkoProject.Data;
using FunkoProject.Data.Entities;
using FunkoProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FunkoProject.Repositories;

[Injectable]
public interface IUserRepository
{
    User Get(int userId);   
    void AddFriend(User user, User friend);
    void SaveChanges();
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

    public void AddFriend(User user, User friend)
    {
        user.Friends.Add(new UserFriend { UserId = user.Id, FriendId = friend.Id });
        friend.FriendOf.Add(new UserFriend { UserId = user.Id, FriendId = friend.Id });
        SaveChanges();
    }

    public void SaveChanges()
    {
        _appDbContext.SaveChanges();
    }
}