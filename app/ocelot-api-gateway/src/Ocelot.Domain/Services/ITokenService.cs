namespace Ocelot.Domain.Services;

public interface ITokenService
{
    string CreateToken(Guid userId, string userEmail);
}