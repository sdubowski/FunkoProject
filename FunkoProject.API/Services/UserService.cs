using FunkoProject.Data.Entities;
using FunkoProject.Models;
using FunkoProject.Repositories;

namespace FunkoProject.Services;

public interface IUserService
{
    User GetUser(int userId);
}

public class UserService: IUserService
{
    private static IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public User GetUser(int userId)
    {
        return _userRepository.Get(userId);
    }
}