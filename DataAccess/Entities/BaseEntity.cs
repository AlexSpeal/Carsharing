namespace DataAccess.Entities;

public class BaseEntity
{
    public long Id { get; set; }

    public Guid ExternalId { get; set; }
    public DateTime ModificationTime { get; set; }
    public DateTime CreationTime { get; set; }
}