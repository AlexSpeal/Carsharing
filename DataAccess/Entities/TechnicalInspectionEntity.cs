using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("TechnicalInspections")]
public class TechnicalInspectionEntity : BaseEntity
{
    public DateTime InspectionStart { get; set; }
    public DateTime InspectionEnd { get; set; }
    public string InspectionResult { get; set; }

    public int MaintenanceStateId { get; set; }
    public virtual StateEntity State { get; set; }

    public int TechnicianId { get; set; }
    public virtual UserEntity TechnicalEmployee { get; set; }

    public int CarId { get; set; }
    public virtual CarEntity Car { get; set; }
}