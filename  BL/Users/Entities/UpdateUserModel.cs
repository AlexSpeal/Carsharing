namespace BL.Users.Entities;

public class UpdateUserModel
{
    public string? Login { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    
    public long RoleId { get; set; }
    public long StateId { get; set; }
}