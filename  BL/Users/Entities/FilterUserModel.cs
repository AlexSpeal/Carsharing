namespace BL.Users.Entities;

public class FilterUserModel
{
    public string? Login { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    
    public DateTime ModificationTime { get; set; }
    public DateTime CreationTime { get; set; }
    public long? RoleId { get; set; }
    public long? StateId { get; set; }
}