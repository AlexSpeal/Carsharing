using BL.Users.Entities;

namespace Service.Controllers.Users.Entities;

public class UsersListResponse
{
    public List<UserModel> Users { get; set; }
}