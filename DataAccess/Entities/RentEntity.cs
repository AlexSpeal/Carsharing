using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("Rent")]
public class RentEntity : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Cost { get; set; }

    public long CarId { get; set; }
    public virtual CarEntity Car { get; set; }

    public long UserId { get; set; }
    public virtual UserEntity User { get; set; }

    public long StateId { get; set; }
    public virtual StateEntity State { get; set; }
}