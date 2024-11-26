namespace DataAccess.Entities;

public class CustomerEntity:UserEntity
{
    public virtual ICollection<DriverLicenseEntity> DriverLicenses { get; set; }
    public virtual ICollection<RentEntity>? Rents { get; set; }
}