using FunkoProject.Data.Entities;
using FunkoProject.Extensions;
using FunkoProject.Models;
using FunkoProject.Repositories;

namespace FunkoProject.Services;

public interface IUserService
{
    UserModel GetUser(int userId);
}

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public UserModel GetUser(int userId)
    {
        return _userRepository.Get(userId).ToModel<User, UserModel>();
    }
}