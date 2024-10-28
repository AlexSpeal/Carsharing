using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("Role")]
public class RoleEntity : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<UserEntity>? Users { get; set; }
}