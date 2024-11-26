namespace BL.Users.Entities;

public class FilterUserModel
{
    public string? Login { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? Patronymic { get; set; }
    public string? Email { get; set; }
    
    public DateTime ModificationTime { get; set; }
    public DateTime CreationTime { get; set; }
    public long? RoleId { get; set; }
    public long? StateId { get; set; }
}