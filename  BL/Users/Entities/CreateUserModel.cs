namespace BL.Users.Entities;

public class CreateUserModel
{
    public string Login { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? Patronymic { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    
    public long RoleId { get; set; }
    public long DriverLicenseId { get; set; }
    public long StateId { get; set; }
}