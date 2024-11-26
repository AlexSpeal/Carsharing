namespace BL.Users.Entities;

public class UserModel
{
    public long Id { get; set; }

    public Guid ExternalId { get; set; }
    public DateTime ModificationTime { get; set; }
    public DateTime CreationTime { get; set; }
    public string FullName { get; set; }
    public string? Patronymic { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    
    public long RoleId { get; set; }
    public long StateId { get; set; }
}