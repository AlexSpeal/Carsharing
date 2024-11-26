using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("User")]
public class UserEntity : BaseEntity
{
    public string Login { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Patronymic { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public long RoleId { get; set; }
    public virtual RoleEntity Role { get; set; }
    
    public virtual ICollection<DriverLicenseEntity> DriverLicenses { get; set; }

    public long StateId { get; set; }
    public virtual StateEntity State { get; set; }

    public virtual ICollection<TechnicalInspectionEntity>? TechnicalInspections { get; set; }

    public virtual ICollection<RentEntity>? Rents { get; set; }
}