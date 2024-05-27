namespace Ocelot.Domain.Services.Account;

public record ValidatePasswordResponse
{
    public required Guid UserId { get; set; }
    public required string Email { get; set; }
}
