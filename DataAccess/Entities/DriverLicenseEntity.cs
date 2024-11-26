using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("DriverLicense")]
public class DriverLicenseEntity : BaseEntity
{
    public string Series { get; set; }
    public string Number { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Category { get; set; }

    public long UserId { get; set; }
    public virtual  UserEntity User { get; set; }
}