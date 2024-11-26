using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("TechnicalInspections")]
public class TechnicalInspectionEntity : BaseEntity
{
    public DateTime InspectionStart { get; set; }
    public DateTime InspectionEnd { get; set; }
    public string InspectionResult { get; set; }

    public long MaintenanceStateId { get; set; }
    public virtual StateEntity State { get; set; }

    public long TechnicianId { get; set; }
    public virtual UserEntity TechnicalEmployee { get; set; }

    public long CarId { get; set; }
    public virtual CarEntity Car { get; set; }
}