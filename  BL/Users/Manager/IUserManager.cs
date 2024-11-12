using BL.Users.Entities;
using DataAccess.Entities;
namespace BL.Users.Manager;

public interface IUserManager
{
    UserModel CreateUser(CreateUserModel createModel);
    void DeleteUser(int id);
    UserModel UpdateUser(UpdateUserModel updateModel);
}