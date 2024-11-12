namespace Service.Controllers.Users.Entities;

public class RegisterUserRequest
{
    public string Login { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? Patronymic { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    
    public int RoleId { get; set; }
    public int DriverLicenseId { get; set; }
    public int StateId { get; set; }
}