namespace DataAccess.Entities;

public class EmployeeEntity:UserEntity
{
    public virtual ICollection<TechnicalInspectionEntity>? TechnicalInspections { get; set; }
}