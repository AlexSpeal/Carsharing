using BL.Authorization.Entities;
using BL.Users.Entities;
using DataAccess.Entities;

namespace BL.Authorization;

public interface IAuthProvider
{
     Task<UserModel> RegisterUser(string fullName,string login, string email, string password);
    Task<TokensResponse> AuthorizeUser(string email, string password);
}