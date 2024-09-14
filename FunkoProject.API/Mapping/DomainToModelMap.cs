using AutoMapper;
using FunkoProject.Data.Entities;
using FunkoProject.Models;

namespace FunkoProject.Data;

public class DomainToModelMap: Profile
{
    public DomainToModelMap()
    {
        CreateMap<User, UserModel>()
            .ForMember(dest => dest.Friends, opt => opt.MapFrom(src => src.Friends.Select(f => f.Friend)));
    }
    
}