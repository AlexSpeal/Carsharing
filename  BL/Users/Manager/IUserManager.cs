using BL.Users.Entities;
using DataAccess.Entities;
namespace BL.Users.Manager;

public interface IUserManager
{
    UserModel CreateUser(CreateUserModel createModel);
    void DeleteUser(long id);
    UserModel UpdateUser(UpdateUserModel updateModel, long id);
}