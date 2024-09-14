using AutoMapper;
using FunkoProject.Data.Entities;
using FunkoProject.Models;

namespace FunkoProject.Data;

public class ModelToDomainMap: Profile
{
    public ModelToDomainMap()
    {
        CreateMap<UserModel, User>()
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.RoleId, opt => opt.Ignore())
            .ForMember(dest => dest.Friends, opt => opt.Ignore())
            .AfterMap((src, dest, context) =>
            {
                if (src.Friends.Any())
                {
                    dest.Friends = src.Friends.Select(f => new UserFriend
                    {
                        UserId = dest.Id,
                        FriendId = f.Id,
                        Friend = context.Mapper.Map<User>(f)
                    }).ToList();
                }
            });
    }
    
}