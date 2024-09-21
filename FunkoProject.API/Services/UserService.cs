using FunkoProject.Common.Atributes;
using FunkoProject.Data.Entities;
using FunkoProject.Exceptions;
using FunkoProject.Extensions;
using FunkoProject.Models;
using FunkoProject.Repositories;
using FunkoProject.Services.Validation;

namespace FunkoProject.Services;

public interface IUserService
{
    UserModel GetUserById(int userId);
    ServiceMessage<User> AddFriend(int userId, int friendId);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserValidationService _userValidationService;

    public UserService(IUserRepository userRepository, IUserValidationService userValidationService)
    {
        _userRepository = userRepository;
        _userValidationService = userValidationService;
    }

    public UserModel GetUserById(int userId)
    {
        var user = _userRepository.Get(userId).ToModel<User, UserModel>();
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        return user;
    }

    public ServiceMessage<User> AddFriend(int userId, int friendId)
    {
        var serviceMessage = new ServiceMessage<User>();
        var userModel = GetUserById(userId);
        var friendModel = GetUserById(friendId);
        var errorMessages = _userValidationService.ValidateFriend(userModel, friendModel);
        if (!errorMessages.Any())
        {
            _userRepository.AddFriend(userModel.ToEntity<UserModel, User>(), friendModel.ToEntity<UserModel, User>());
        }
        else
        {
            serviceMessage.ErrorMessages.AddRange(errorMessages);
        }

        return serviceMessage;
    }
}