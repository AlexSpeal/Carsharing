using BL.Users.Entities;
using DataAccess.Entities;
using DataAccess.Repository;
using AutoMapper;
using BL.Users.Exceptions;

namespace BL.Users.Manager;

public class UserManager:IUserManager
{
    private readonly IRepository<UserEntity> _usersRepository;
    private readonly IMapper _mapper;
    
    public UserManager(IRepository<UserEntity> usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    
    public UserModel CreateUser(CreateUserModel createModel)
    {
        var entity = _mapper.Map<UserEntity>(createModel);
        entity = _usersRepository.Save(entity);
        return _mapper.Map<UserModel>(entity);
    }

    public void DeleteUser(int id)
    {
        try
        {
            var entity = _usersRepository.GetById(id);
            _usersRepository.Delete(entity);
        }
        catch (Exception e)
        {
            throw new UserNotFoundException(e.Message);
        }
    }

    public UserModel UpdateUser(UpdateUserModel updateModel)
    {
        var entity = _mapper.Map<UserEntity>(updateModel);
        entity = _usersRepository.Save(entity);
        return _mapper.Map<UserModel>(entity);
    }
}