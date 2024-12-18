using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities;

[Table("User")]
public class UserEntity : IdentityUser<int>, IBaseEntity
{
    public long Id { get; set; }
    public Guid ExternalId { get; set; }
    public DateTime ModificationTime { get; set; }
    public DateTime CreationTime { get; set; }
    public string FullName { get; set; }
    public long StateId { get; set; }
    public virtual StateEntity State { get; set; }
    
}
public class UserRole : IdentityRole<int>
{
}