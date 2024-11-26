using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("Car")]
public class CarEntity : BaseEntity
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int YearOfManufacture { get; set; }
    public string LicensePlate { get; set; }

    public long StateId { get; set; }
    public virtual StateEntity State { get; set; }

    public virtual ICollection<RentEntity>? Rents { get; set; }
    public virtual ICollection<TechnicalInspectionEntity>? TechnicalInspections { get; set; }
}