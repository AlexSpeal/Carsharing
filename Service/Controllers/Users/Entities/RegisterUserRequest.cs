namespace Service.Controllers.Users.Entities;

public class RegisterUserRequest
{
    public string Login { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public int RoleId { get; set; }
    public int StateId { get; set; }
}