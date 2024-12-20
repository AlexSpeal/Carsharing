﻿using BL.Users.Entities;
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

    public void DeleteUser(long id)
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

    public UserModel UpdateUser(UpdateUserModel model, long userId)
    {
     
        var entity = _usersRepository.GetById(userId);
        if (entity == null)
        {
            throw new UserNotFoundException("User not found");
        }
        
        entity = _mapper.Map<UpdateUserModel, UserEntity>(model, opts => 
            opts.AfterMap(
            (src, dest) =>
            {
                
                dest.Id = userId;
                dest.ExternalId = entity.ExternalId;
                dest.CreationTime = entity.CreationTime;
                dest.ModificationTime=DateTime.UtcNow;
                dest.StateId=model.StateId;
                dest.UserName = src.Login ?? entity.UserName;
                dest.PasswordHash = src.PasswordHash ?? entity.PasswordHash;
                dest.Email = src.Email ?? entity.Email;
                dest.FullName = src.FullName ?? entity.FullName;
            }
        ));
        
        _usersRepository.Save(entity);
        
        return  _mapper.Map<UserModel>(entity);
    }


}