using AutoMapper;
using BL.Users.Entities;
using Service.Controllers.Users.Entities;

namespace Service.Controllers.Mapper;

public class UsersServiceProfile : Profile
{
    public UsersServiceProfile()
    {
        CreateMap<UserFilter, FilterUserModel>();
        CreateMap<RegisterUserRequest, CreateUserModel>();
    }
}