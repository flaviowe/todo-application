
namespace Account.Domain.Entities;

public class User : BaseEntity
{
    public required string Name { get; set; }
    public required string Password { get; set; }
}