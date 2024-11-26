using AutoMapper;
using BL.Users.Entities;
using BL.Users.Exceptions;
using DataAccess.Entities;
using DataAccess.Repository;

namespace BL.Users.Provider;

public class UserProvider : IUserProvider
{
    private readonly IMapper _mapper;
    private readonly IRepository<UserEntity> _usersRepository;
    
    public UserProvider(IRepository<UserEntity> userRepository, IMapper mapper)
    {
        _usersRepository = userRepository;
        _mapper = mapper;
    }
    public IEnumerable<UserModel> GetUsers(FilterUserModel filter = null)
    {
        var login = filter?.Login;

        var fullName = filter?.FullName;
        var email = filter?.Email;
        var stateId = filter?.StateId;
        var modificationTime = filter?.ModificationTime;
        var creationTime=filter?.CreationTime;

        var users = _usersRepository.GetAll(u =>
            (login == null || u.Login == login)
            && (fullName == null || u.FullName == fullName)
            && (stateId == null || u.StateId == stateId)
            && (email == null || u.Email == email)
            && (creationTime == null || u.CreationTime == creationTime)
            && (modificationTime == null || u.ModificationTime == modificationTime));
        
        return _mapper.Map<IEnumerable<UserModel>>(users);
    }

    public UserModel GerUserInfo(long id)
    {
        try
        {
            var entity = _usersRepository.GetById(id);
            return _mapper.Map<UserModel>(entity);
        }
        catch (Exception ex)
        {
            throw new UserNotFoundException(ex.Message);
        }
    }
}