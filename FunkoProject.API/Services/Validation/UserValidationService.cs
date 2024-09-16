using FunkoProject.Common.Enums;
using FunkoProject.Data.Entities;
using FunkoProject.Models;

namespace FunkoProject.Services.Validation;

public interface IUserValidationService
{
    List<ErrorMessage> ValidateFriend(UserModel user, UserModel friend);
}
[Injectable]
public class UserValidationService: IUserValidationService
{
    public List<ErrorMessage> ValidateFriend(UserModel user, UserModel friend)
    {
        var errorMessages = new List<ErrorMessage>();
        if (friend == null)
        {
            var errorMessage = new ErrorMessage
            {
                Id = "friend_1",
                ErrorType = ErrorTypeEnum.Error,
                Error = "Friend not found!"
            };
            errorMessages.Add(errorMessage);
        }
        else
        {
            if (user.Friends.Contains(friend))
            {
                var errorMessage = new ErrorMessage
                {
                    Id = "friend_2",
                    ErrorType = ErrorTypeEnum.Error,
                    Error = "Friend is already on a list!"
                };
                errorMessages.Add(errorMessage);
            }
        }
        
        
        return errorMessages;
    }
}