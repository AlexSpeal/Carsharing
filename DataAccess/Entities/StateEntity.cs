using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("States")]
public class StateEntity : BaseEntity
{
    public string State { get; set; }

    public virtual ICollection<UserEntity>? Users { get; set; }
    public virtual ICollection<RentEntity>? Rents { get; set; }
    public virtual ICollection<TechnicalInspectionEntity>? TechnicalInspections { get; set; }
    public virtual ICollection<CarEntity>? Cars { get; set; }
}