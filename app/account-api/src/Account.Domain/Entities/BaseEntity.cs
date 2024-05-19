namespace Account.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? UpdateBy { get; set; }
    public DateTime? UpdateAt { get; set; }
}