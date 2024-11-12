namespace Service.Controllers.Users.Entities;

public class UserFilter
{
    public string? Login { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? Patronymic { get; set; }
    public string? Email { get; set; }
    
    public DateTime ModificationTime { get; set; }
    public DateTime CreationTime { get; set; }
    public int? RoleId { get; set; }
    public int? StateId { get; set; }
}