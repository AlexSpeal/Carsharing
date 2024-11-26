using BL.Users.Entities;

namespace BL.Users.Provider;

public interface IUserProvider
{
    IEnumerable<UserModel> GetUsers(FilterUserModel filter = null);
    UserModel GerUserInfo(long id);
}